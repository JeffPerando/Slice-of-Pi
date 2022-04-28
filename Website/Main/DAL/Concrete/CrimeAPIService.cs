
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;
using Main.Helpers;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class CrimeAPIService : ICrimeAPIService
    {
        public static readonly int LatestYear = 2020;
        public static readonly int OldestYear = 1985;

        private readonly string keyFBI = "";
        private readonly IWebService _web;

        //public readonly string state_json = "https://gist.githubusercontent.com/mshafrir/2646763/raw/8b0dbb93521f5d6889502305335104218454c2bf/states_titlecase.json";

        private static readonly string crime_api_state_info = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states";
        private static readonly string crime_statistics_api_url = "https://api.usa.gov/crime/fbi/sapi/api/agencies/byStateAbbr";
        private static readonly string crime_url_agency_reported_crime = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies";

        public CrimeAPIService(IConfiguration config, IWebService web) : this(config["apiFBIKey"], web) { }

        public CrimeAPIService(string token, IWebService web)
        {
            keyFBI = token.Split("=").Last();
            _web = web;

        }

        private JObject? FetchFBIObj(string url)
        {
            return _web.FetchJObject(url, new()
            {
                ["API_KEY"] = keyFBI
            });
        }

        private Task<JObject?> FetchFBIObjAsync(string url)
        {
            return _web.FetchJObjectAsync(url, new()
            {
                ["API_KEY"] = keyFBI
            });
        }

        private JArray? FetchFBIArray(string url)
        {
            return _web.FetchJArray(url, new()
            {
                ["API_KEY"] = keyFBI
            });
        }

        private JObject? FetchStateAgencies(string state)
        {
            string url = $"{crime_statistics_api_url}/{state}";

            return FetchFBIObj(url);
        }

        private string? GetCityORI(string city, string state)
        {
            var info = FetchStateAgencies(state);

            if (info == null)
            {
                return null;
            }

            var results = info["results"];

            if (results == null || !results.Any())
            {
                return null;
            }

            foreach (var item in results)
            {
                if ((item["agency_name"] ?? "").ToString() == $"{city} Police Department")
                {
                    return item["ori"]?.ToString();
                }

            }

            return null;
        }

        private JObject? FetchCityData(string city, string state, string? year = null)
        {
            if (year == null)
            {
                year = LatestYear.ToString();
            }

            var ori = GetCityORI(city, state);

            var url = $"{crime_url_agency_reported_crime}/{ori}/offenses/{year}/{year}";
            return FetchFBIObj(url);
        }

        public List<string> GetStates()
        {
            throw new NotImplementedException();
            //return states.Select(s => s.Abbrev).ToList();
        }

        public List<Crime> ReturnStateCrimeList(List<string> states)
        {
            var fetches = new List<Task<Crime?>>();
            var states_crime = new List<Crime>();

            foreach (var state in states)
            {
                fetches.Add(GetOverallStateCrimeAsync(state));

            }

            foreach (var fetch in fetches)
            {
                var crimes = fetch.GetAwaiter().GetResult();

                if (crimes != null)
                {
                    states_crime.Add(crimes);
                }

            }

            return states_crime;
        }

        public async Task<Crime?> GetOverallStateCrimeAsync(string state)
        {
            //idk why this is here
            var year = new JSONYearVariable();

            try
            {
                var url = $"{crime_api_state_info}/{state}/{year.setYearForJSON(0)}";

                var info = await FetchFBIObjAsync(url);

                if (info == null)
                {
                    return null;
                }

                int population = (int)info["results"][0]["population"];
                int total_crime = (int)info["results"][0]["violent_crime"] + (int)info["results"][0]["property_crime"];

                return new Crime { State = state, Population = population, TotalOffenses = total_crime };
            }
            catch
            {
                return null;
            }

        }

        public List<Crime> GetSafestStates(List<Crime> crimeList)
        {
            var top_five_states = crimeList.OrderBy(c => c.CrimePerCapita).Take(5).ToList();

            return top_five_states;
        }

        public List<Crime> GetCityStatsByYear(string cityName, string stateAbbrev, string year)
        {
            var city_crime_stats = new List<Crime>();

            var city_stats = FetchCityData(cityName, stateAbbrev, year);

            if (city_stats == null)
            {
                return city_crime_stats;
            }

            foreach (var crime in city_stats["results"])
            {
                var offense = (crime["offense"] ?? "UNKNOWN").ToString();

                if (offense == "property-crime" || offense == "violent-crime" || offense == "arson")
                {
                    //Why???
                    continue;
                }

                // Will get stuff like "data_year", "ori", "actual" meaning real crimes, "offense" meaning the type, and "cleared" for reported and dealt with.

                //string ori = (string)crime["ori"];

                city_crime_stats.Add(new Crime
                {
                    Year = (int)crime["data_year"],
                    OffenseType = offense,
                    TotalOffenses = (int)crime["actual"] + (int)crime["cleared"],
                    ActualConvictions = (int)crime["actual"],
                    State = stateAbbrev
                });
            }

            return city_crime_stats.OrderBy(c => c.TotalOffenses).ToList();
        }

        public List<Crime> GetCityStats(string cityName, string stateAbbrev)
        {
            var info = FetchFBIObj($"{crime_statistics_api_url}/{stateAbbrev}");

            var city_crime_stats = new List<Crime>();
            var fetches = new List<Task<List<Crime?>>>();

            foreach (var city in info["results"])
            {
                fetches.Add(GetCityStatsAsync(city, cityName));
            }

            foreach (var fetch in fetches)
            {
                var city_information = fetch.GetAwaiter().GetResult();

                if (city_information != null)
                {
                    foreach (var item in city_information)
                    {
                        city_crime_stats.Add(item);
                    }
                }
            }

            return city_crime_stats.OrderBy(c => c.TotalOffenses).ToList();
        }

        public async Task<List<Crime?>> GetCityStatsAsync(JToken city, string cityName)
        {
            var searchedCity = $"{cityName} Police Department";
            var cityJTokenName = (string)(city["agency_name"]);

            var year = new JSONYearVariable();
            var city_crime_stats = new List<Crime>();

            if (searchedCity == cityJTokenName)
            {
                var city_stats = await FetchFBIObjAsync(crime_url_agency_reported_crime + city["ori"] + "/offenses/" + year.setYearForJSON(0));

                foreach (var crime in city_stats["results"])
                {
                    var offense = crime["offense"]?.ToString() ?? "UNKNOWN";
                    if (offense == "property-crime" || offense == "violent-crime" || offense == "arson" || offense == "rape-legacy")
                    {
                        continue;
                    }
                    // Will get stuff like "data_year", "ori", "actual" meaning real crimes, "offense" meaning the type, and "cleared" for reported and dealt with.
                    city_crime_stats.Add(new Crime
                    {
                        Year = (int)crime["data_year"],
                        OffenseType = offense,
                        TotalOffenses = (int)crime["actual"] + (int)crime["cleared"],
                        ActualConvictions = (int)crime["actual"],
                        State = (string)crime["state_abbr"]
                    });
                }

            }

            return city_crime_stats;
        }

        public StateCrimeSearchResult? GetState(string stateAbbrev, int? aYear)
        {
            if (aYear == null)
            {
                aYear = 2020;
            }

            var state_crime_stats = new StateCrimeSearchResult();
            var info = FetchFBIObj($"{crime_api_state_info}/{stateAbbrev}/{aYear}/{aYear}");

            if (info == null)
            {
                return null;
            }

            state_crime_stats = state_crime_stats.PresentJSONRespone(info);

            return state_crime_stats;
        }

        public JObject GetCityTrends(string cityName, string stateAbbrev)
        {
            var ori = GetCityORI(cityName, stateAbbrev);

            if (ori == null)
            {
                return null;
            }

            var city_stats = FetchFBIObj($"{crime_url_agency_reported_crime}/{ori}/offenses/{OldestYear}/{LatestYear}");

            return city_stats;
        }

        //FORMATS THE DATA INTO CRIME OBJECT LIST FOR GRAPH DISPLAYING
        public List<Crime> ReturnTotalCityTrends(JObject city_stats)
        {
            var counter = -1;
            var city_crime_trends = new List<Crime>();

            if (city_stats == null)
            {
                return city_crime_trends;
            }

            //This allows for us to only get the amount of property crimes and violent crimes combined since all subcategories of crime fall under both prop crime and violent crime.
            foreach (var crime in city_stats["results"])
            {
                if ((string)crime["offense"] == "property-crime" || (string)crime["offense"] == "violent-crime")
                {
                    if (!city_crime_trends.Any(y => y.Year == (int)crime["data_year"]))
                    {
                        city_crime_trends.Add(new Crime { Year = (int)crime["data_year"], TotalOffenses = (int)crime["actual"] + (int)crime["cleared"] });
                        counter++;
                        continue;
                    }
                    city_crime_trends[counter].TotalOffenses = city_crime_trends[counter].TotalOffenses + (int)crime["actual"] + (int)crime["cleared"];
                }
            }

            return city_crime_trends;
        }

        public List<Crime> ReturnPropertyCityTrends(JObject city_stats)
        {
            if (city_stats == null)
            {
                return null;
            }

            var counter = -1;
            var city_crime_trends = new List<Crime>();

            //This allows for us to only get the amount of property crimes and violent crimes combined since all subcategories of crime fall under both prop crime and violent crime.
            foreach (var crime in city_stats["results"])
            {
                if ((string)crime["offense"] == "property-crime")
                {
                    if (!city_crime_trends.Any(y => y.Year == (int)crime["data_year"]))
                    {
                        city_crime_trends.Add(new Crime { Year = (int)crime["data_year"], TotalOffenses = (int)crime["actual"] + (int)crime["cleared"], OffenseType = (string)crime["offense"] });
                        counter++;
                        continue;
                    }
                    city_crime_trends[counter].TotalOffenses = city_crime_trends[counter].TotalOffenses + (int)crime["actual"] + (int)crime["cleared"];
                }
            }
            return city_crime_trends;
        }

        public List<Crime> ReturnViolentCityTrends(JObject city_stats)
        {
            var city_crime_trends = new List<Crime>();

            if (city_stats == null)
            {
                return city_crime_trends;
            }

            var counter = -1;

            //This allows for us to only get the amount of property crimes and violent crimes combined since all subcategories of crime fall under both prop crime and violent crime.
            foreach (var crime in city_stats["results"])
            {
                if ((string)crime["offense"] == "violent-crime")
                {
                    if (!city_crime_trends.Any(y => y.Year == (int)crime["data_year"]))
                    {
                        city_crime_trends.Add(new Crime { Year = (int)crime["data_year"], TotalOffenses = (int)crime["actual"] + (int)crime["cleared"], OffenseType = (string)crime["offense"] });
                        counter++;
                        continue;
                    }

                    city_crime_trends[counter].TotalOffenses = city_crime_trends[counter].TotalOffenses + (int)crime["actual"] + (int)crime["cleared"];
                }
            }

            return city_crime_trends;
        }

        public int GetCityCount(string state)
        {
            var agencies = FetchStateAgencies(state);
            var results = agencies?["results"];

            if (results == null)
            {
                return 0;
            }

            return results.Where(agency => agency["agency-type"]?.ToString().ToLower() == "city").Count();
        }

        public int? GetTotalCityCrime(string city, string state)
        {
            var data = FetchCityData(city, state);

            if (data == null)
            {
                return null;
            }

            return data?["results"]?
                .Where(r => {
                    var type = r?["offense-type"]?.ToString();
                    return (type == "violent-crime" || type == "property-crime");
                }).Sum(r => (int)(r?["actual"] ?? 0));
        }

    }

}