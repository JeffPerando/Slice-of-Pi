
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.FBI;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class FBIService : ICrimeAPIv2
    {
        public static readonly int LatestYear = 2020;
        public static readonly int OldestYear = 1985;

        //private static readonly string fbiBaseURL = "https://api.usa.gov/crime/fbi/sapi/api/";
        private static readonly string agency_list_url = "https://api.usa.gov/crime/fbi/sapi/api/agencies/byStateAbbr";
        private static readonly string state_crime_url = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states";
        private static readonly string city_crime_url = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies";
        private static readonly string national_crime_url = "https://api.usa.gov/crime/fbi/sapi/api/estimates/national";

        //Members beyond this point

        private readonly string _key;
        private readonly IWebService _web;

        public FBIService(IConfiguration config, IWebService web) : this(config["apiFBIKey"], web) { }

        public FBIService(string key, IWebService web)
        {
            _key = key.Split("=").Last();
            _web = web;

        }


        //Helpers

        private JObject? FetchFBIObj(string url)
        {
            return _web.FetchJObject(url, new()
            {
                ["API_KEY"] = _key
            });
        }

        private Task<JObject?> FetchFBIObjAsync(string url)
        {
            return _web.FetchJObjectAsync(url, new()
            {
                ["API_KEY"] = _key
            });
        }

        private JArray? FetchStateAgencies(string state)
        {
            string url = $"{agency_list_url}/{state}";

            var data = FetchFBIObj(url);

            var results = data?["results"];

            if (results == null || !results.Any())
            {
                return null;
            }

            return (JArray)results;
        }

        private string? FetchCityORI(string city, string state)
        {
            var agencies = FetchStateAgencies(state);

            if (agencies == null)
                return null;

            foreach (var item in agencies)
            {
                if ((item["agency_name"] ?? "").ToString() == $"{city} Police Department")
                {
                    return item["ori"]?.ToString();
                }

            }

            return null;
        }

        private List<string>? FetchCitiesORI(List<string> cities, string state)
        {
            var agencies = FetchStateAgencies(state);

            if (agencies == null)
                return null;

            var cityList = agencies.Where(a => a["agency_type_name"]?.ToString() == "City").ToList();

            if (cityList.Count > cities.Count)
            {
                throw new Exception($"More cities are being sought after than exist: {cities.Count} vs. {cityList.Count}");
            }

            if (cityList.Count == cities.Count)
            {
                return cityList.Where(city => city["ori"] != null).Select(city => city["ori"].ToString()).ToList();
            }

            var cityORIs = new List<string>();

            foreach (var item in agencies)
            {
                foreach (var city in cities)
                {
                    if ((item["agency_name"] ?? "").ToString() == $"{city} Police Department")
                    {
                        var ori = item["ori"]?.ToString();
                        if (ori != null)
                        {
                            cityORIs.Add(ori);
                            break;
                        }

                    }

                }

            }

            return null;
        }


        //State crime stats

        public async Task<StateCrimeStats?> StateCrimeSingleAsync(State state, int? year = null)
        {
            var results = (await FetchFBIObjAsync($"{state_crime_url}/{state.Abbrev}/{year ?? LatestYear}/{year ?? LatestYear}"))?["results"];
            
            if (results == null)
                return null;

            if (!results.Any())
                return null;

            return new StateCrimeStats(state, (JObject)results[0]);
        }

        public List<StateCrimeStats?> StateCrimeMulti(List<State> states, int? year = null)
        {
            var fetches = states.Select(state => StateCrimeSingleAsync(state, year)).ToArray();

            Task.WaitAll(fetches);

            return fetches.Select(t => t.GetAwaiter().GetResult()).ToList();
        }

        public List<StateCrimeStats>? StateCrimeRange(State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{state_crime_url}/{state.Abbrev}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return null;

            if (!results.Any())
                return null;

            return results.Select(t => new StateCrimeStats(state, (JObject)t)).ToList();
        }

        public List<BasicCrimeStats>? StateCrimeRangeBasic(State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{state_crime_url}/{state.Abbrev}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return null;

            if (!results.Any())
                return null;

            return results.Select(t => new BasicCrimeStats((JObject)t)).ToList();
        }


        //City crime stats

        //Let it be known that I will have nightmares about how the FBI reports its city crime stats
        //And the subtle differences between this API and the state one
        public List<CityCrimeStats>? CityCrimeRange(string city, State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var ori = FetchCityORI(city, state.Abbrev);

            if (ori == null)
                return null;

            var results = FetchFBIObj($"{city_crime_url}/{ori}/{fromYear}/{toYear}")?["results"];
            
            if (results == null)
                return null;

            var crimes = new List<CityCrimeStats>();

            for (int year = fromYear; year <= toYear; year++)
            {
                crimes.Add(new CityCrimeStats(city, state, year, (JArray)results));

            }

            return crimes;
        }

        public List<BasicCrimeStats>? CityCrimeRangeBasic(string city, State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var ori = FetchCityORI(city, state.Abbrev);

            if (ori == null)
                return null;

            var results = FetchFBIObj($"{city_crime_url}/{ori}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return null;

            var crimes = new List<BasicCrimeStats>();

            for (int year = fromYear; year <= toYear; year++)
            {
                crimes.Add(new BasicCrimeStats(year, (JArray)results));

            }

            return crimes;
        }


        //National crime stats

        public List<NationalCrimeStats>? NationalCrimeRange(int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{national_crime_url}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return null;

            return results.Select(t => new NationalCrimeStats((JObject)t)).ToList();
        }

        public List<BasicCrimeStats>? NationalCrimeRangeBasic(int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{national_crime_url}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return null;

            return results.Select(t => new BasicCrimeStats((JObject)t)).ToList();
        }


        //Misc.

        //Find all cities in a state
        public List<string>? CitiesIn(State state)
        {
            var agencies = FetchStateAgencies(state.Abbrev);

            if (agencies == null)
                return null;

            return agencies.Where(a => a["agency_type_name"]?.ToString() == "City").Select(a =>
            {
                var name = a["agency-name"]?.ToString() ?? "";

                return name.Substring(0, Math.Abs(name.LastIndexOf(" Police Department")));
            }).ToList();
        }

    }

}
