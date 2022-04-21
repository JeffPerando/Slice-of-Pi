using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Main.DAL.Concrete
{
    public class CrimeAPIService : ICrimeAPIService
    {
        private readonly string keyFBI = "";
        private readonly IWebService _web;

        public readonly string state_json = "https://gist.githubusercontent.com/mshafrir/2646763/raw/8b0dbb93521f5d6889502305335104218454c2bf/states_titlecase.json";
        public readonly string crime_api_state_info = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states/";
        public readonly string crime_statistics_api_url = "https://api.usa.gov/crime/fbi/sapi/api/agencies/byStateAbbr/";
        public readonly string crime_url_agency_reported_crime = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies/";

        // public string crime_state_api_url { get; }

        //old unit tests don't like this, please don't use
        //and turns out it breaks the app entirely. commenting out...
        //[Obsolete("Please clean this up. Unit tests that use this class are actually integration tests. Move things that don't need to contact the FBI somewhere else, like a service or the API controller")]
        //public CrimeAPIService() : this("INVALID_KEY", new WebService()) {}

        public CrimeAPIService(IConfiguration config) : this(config["apiFBIKey"], new WebService()) { }

        public CrimeAPIService(string token) : this(token, new WebService()) { }

        public CrimeAPIService(string token, IWebService web)
        {
            keyFBI = token.Split("=").Last();
            _web = web;

            //note: DOES NOT WORK WITH THIS API, DO NOT USE.
            //_web.AddHeader("API_KEY", keyFBI);

        }

        private string AddAPIKey(string url)
        {
            return $"{url}?API_KEY={keyFBI}";
        }

        private JObject? FetchFBIObj(string url)
        {
            return _web.FetchJObject(AddAPIKey(url));
        }

        private Task<JObject?> FetchFBIObjAsync(string url)
        {
            return _web.FetchJObjectAsync(AddAPIKey(url));
        }

        private JArray? FetchFBIArray(string url)
        {
            return _web.FetchJArray(AddAPIKey(url));
        }

        public List<string> GetStates()
        {
            var info = FetchFBIArray(state_json);
            List<string> state_abbrevs = new List<string>();

            for (int i = 0; i < info.Count; i++)
            {
                if ((string)info[i]["abbreviation"] == "AS" || (string)info[i]["abbreviation"] == "FM" || (string)info[i]["abbreviation"] == "GU" ||
                (string)info[i]["abbreviation"] == "MH" || (string)info[i]["abbreviation"] == "MP" || (string)info[i]["abbreviation"] == "PW" || 
                (string)info[i]["abbreviation"] == "PR")
                {
                    continue;
                }
                string abbreviation = (string)info[i]["abbreviation"];
                state_abbrevs.Add(abbreviation);
            }
            return state_abbrevs;
        }

        public List<Crime> ReturnStateCrimeList(List<string> states)
        {
            JSONYearVariable year = new JSONYearVariable();
            List<Crime> states_crime = new List<Crime>();
            var fetches = new List<Task<Crime?>>();


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
            JSONYearVariable year = new JSONYearVariable();

            try
            {
                var url = crime_api_state_info + state + year.setYearForJSON(0);
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
            List<Crime> city_crime_stats = new List<Crime>();

            var info = FetchFBIObj(crime_statistics_api_url + stateAbbrev);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {
                    //TODO two "years" here look redundant and weird.
                    var url = $"{crime_url_agency_reported_crime}{item["ori"]}/offenses/{year}/{year}";
                    var city_stats = FetchFBIObj(url);

                    foreach (var crime in city_stats["results"])
                    {
                        if ((string)crime["offense"] == "property-crime" || (string)crime["offense"] == "violent-crime" || (string)crime["offense"] == "arson")

                        {
                            continue;
                        }
                        // Will get stuff like "data_year", "ori", "actual" meaning real crimes, "offense" meaning the type, and "cleared" for reported and dealt with.
                        int crime_year = (int)crime["data_year"];
                        string ori = (string)crime["ori"];
                        string state_abbr = (string)crime["state_abbr"];
                        string agency_name = text;
                        string offense_type = (string)crime["offense"];
                        int actual_convictions = (int)crime["actual"];
                        int total_offenses = (int)crime["actual"] + (int)crime["cleared"];

                        city_crime_stats.Add(new Crime
                        {
                            Year = crime_year,
                            OffenseType = offense_type,
                            TotalOffenses = total_offenses,
                            ActualConvictions = actual_convictions,
                            State = state_abbr
                        });
                    }
                    //Stops after it finds what it needs.
                    break;
                }
            }
            return city_crime_stats;
        }

        public List<Crime> GetCityStats(string cityName, string stateAbbrev)
        {
            var info = FetchFBIObj(crime_statistics_api_url + stateAbbrev);

            JSONYearVariable year = new JSONYearVariable();
            List<Crime> city_crime_stats = new List<Crime>();

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {
                    var city_stats = FetchFBIObj(crime_url_agency_reported_crime + item["ori"] + "/offenses" + year.setYearForJSON(0));

                    foreach(var crime in city_stats["results"])
                    {
                        Debug.WriteLine(crime);
                    }
                    

                    foreach (var crime in city_stats["results"])
                    {
                        if ((string)crime["offense"] == "property-crime" || (string)crime["offense"] == "violent-crime" || (string)crime["offense"] == "arson" || (string)crime["offense"] == "rape-legacy")
                        {
                            continue;
                        }
                        // Will get stuff like "data_year", "ori", "actual" meaning real crimes, "offense" meaning the type, and "cleared" for reported and dealt with.
                        int crime_year = (int)crime["data_year"];
                        string ori = (string)crime["ori"];
                        string state_abbr = (string)crime["state_abbr"];
                        string agency_name = text;
                        string offense_type = (string)crime["offense"];
                        int actual_convictions = (int)crime["actual"];
                        int total_offenses = (int)crime["actual"] + (int)crime["cleared"];

                        city_crime_stats.Add(new Crime
                        {
                            Year = crime_year,
                            OffenseType = offense_type,
                            TotalOffenses = total_offenses,
                            ActualConvictions = actual_convictions,
                            State = state_abbr
                        });
                    }
                    //Stops after it finds what it needs.
                    break;
                }
            }
            return city_crime_stats;
        }

        public List<Crime> ReturnCityStats(List<Crime> city_stats)
        {
            return city_stats.OrderByDescending(t => t.TotalOffenses).ToList();
        }

        public StateCrimeSearchResult GetState(string stateAbbrev, int? aYear)
        {
            var year = "/" + aYear + "/" + aYear;
            var state_crime_stats = new StateCrimeSearchResult();
            Debug.WriteLine(crime_api_state_info + stateAbbrev + year);
            var info = FetchFBIObj(crime_api_state_info + stateAbbrev + year);
            Debug.WriteLine(info);
            state_crime_stats = state_crime_stats.PresentJSONRespone(info);
            //var deserializedProduct = JsonConvert.DeserializeObject<IEnumerable<StateCrimeViewModel>>(jsonResponse);

            return state_crime_stats;
        }

        public JObject GetCityTrends(string cityName, string stateAbbrev)
        {
            JSONYearVariable year = new JSONYearVariable();
            List<Crime> city_crime_trends = new List<Crime>();
            var info = FetchFBIObj(crime_statistics_api_url + stateAbbrev);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {
                    var city_stats = FetchFBIObj($"{crime_url_agency_reported_crime}{item["ori"]}/offenses/{(year.getYearTwoYearsAgo() - 35)}/{year.getYearTwoYearsAgo()}");

                    return city_stats;

                }

            }
            return null;
        }

        //FORMATS THE DATA INTO CRIME OBJECT LIST FOR GRAPH DISPLAYING
        public List<Crime> ReturnTotalCityTrends(JObject city_stats)
        {
            var counter = -1;
            List<Crime> city_crime_trends = new List<Crime>();

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
            List<Crime> city_crime_trends = new List<Crime>();

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
            if (city_stats == null)
            {
                return null;
            }
            var counter = -1;
            List<Crime> city_crime_trends = new List<Crime>();

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
    }
}
