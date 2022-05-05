
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.FBI;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;

namespace Main.DAL.Concrete
{
    public class FBIService : ICrimeAPIv2
    {
        public static readonly int LatestYear = 2020;
        public static readonly int OldestYear = 1985;

        private const string base_url = "https://api.usa.gov/crime/fbi/sapi/api";
        private const string agency_endpoint = "/agencies/byStateAbbr";
        private const string state_crime_endpoint = "/estimates/states";
        private const string city_crime_endpoint = "/summarized/agencies";
        private const string national_crime_endpoint = "/estimates/national";

        //Members beyond this point

        private readonly string _key;
        private readonly IWebService _web;
        private readonly IAPICacheService<FBICache> _cache;

        public FBIService(IConfiguration config, IWebService web, IAPICacheService<FBICache> cache) : this(config["apiFBIKey"], web, cache) { }

        public FBIService(string key, IWebService web, IAPICacheService<FBICache> cache)
        {
            _key = key.Split("=").Last();
            _web = web;
            _cache = cache.SetBaseURL(base_url);
            
        }


        //Helpers

        private JObject? FetchFBIObj(string endpoint)
        {
            return _cache.FetchJObject(endpoint, new()
            {
                ["API_KEY"] = _key
            }, false);
        }

        private Task<JObject?> FetchFBIObjAsync(string endpoint)
        {
            return _web.FetchJObjectAsync(base_url + endpoint, new()
            {
                ["API_KEY"] = _key
            });
        }

        private JArray? FetchStateAgencies(string state)
        {
            var data = FetchFBIObj($"{agency_endpoint}/{state}");

            var results = data?["results"];

            if (results == null || !results.Any())
            {
                return null;
            }

            return (JArray)results;
        }

        private string? FetchORI(string city, string state)
        {
            var agencies = FetchStateAgencies(state);

            if (agencies == null)
                return null;

            foreach (var item in agencies)
            {
                if (item["agency_type_name"]?.ToString().ToLower() != "city")
                {
                    continue;
                }
                
                if ((item["agency_name"] ?? "").ToString().Contains(city))
                {
                    return item["ori"]?.ToString();
                }

            }

            return null;
        }

        private List<(string, string?)>? FetchORIs(List<string> cities, string state)
        {
            var agencies = FetchStateAgencies(state);

            if (agencies == null)
                return null;

            var cityList = agencies.Where(a => a["agency_type_name"]?.ToString() == "City").ToList();

            if (cityList.Count > cities.Count)
            {
                throw new Exception($"More cities are being sought after than exist: {cities.Count} vs. {cityList.Count}");
            }

            var cityORIs = new List<(string, string?)>();

            foreach (var item in agencies)
            {
                if (item["agency_type_name"]?.ToString().ToLower() != "city")
                {
                    continue;
                }

                foreach (var city in cities)
                {
                    if ((item["agency_name"] ?? "").ToString().Contains(city))
                    {
                        var ori = item["ori"]?.ToString();
                        if (ori != null)
                        {
                            cityORIs.Add((city, ori));
                            break;
                        }

                    }

                }

            }

            return cityORIs;
        }


        //State crime stats

        public async Task<StateCrimeStats?> StateCrimeSingleAsync(State state, int? year = null)
        {
            var results = (await FetchFBIObjAsync($"{state_crime_endpoint}/{state.Abbrev}/{year ?? LatestYear}/{year ?? LatestYear}"))?["results"];
            
            if (results == null)
                return null;

            if (!results.Any())
                return null;

            return new StateCrimeStats(state, results[0]);
        }

        public List<StateCrimeStats> StateCrimeMulti(List<State> states, int? year = null)
        {
            var fetches = states.Select(state => StateCrimeSingleAsync(state, year)).ToArray();

            Task.WaitAll(fetches);

            var stats = new List<StateCrimeStats>();

            foreach (var fetch in fetches)
            {
                var stat = fetch.GetAwaiter().GetResult();
                
                if (stat == null)
                    continue;

                stats.Add(stat);
            }

            return stats;
        }

        public List<StateCrimeStats> StateCrimeRange(State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{state_crime_endpoint}/{state.Abbrev}/{fromYear}/{toYear}")?["results"];

            if (!(results?.Any() ?? false))
                return new();

            return results.Select(t => new StateCrimeStats(state, t)).ToList();
        }

        public List<BasicCrimeStats> StateCrimeRangeBasic(State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{state_crime_endpoint}/{state.Abbrev}/{fromYear}/{toYear}")?["results"];

            if (!(results?.Any() ?? false))
                return new();

            return results.Select(t => new BasicCrimeStats(t)).ToList();
        }


        //City crime stats

        //Let it be known that I will have nightmares about how the FBI reports its city crime stats
        //And the subtle differences between this API and the state one
        private JToken? FetchCityData(string? ori, int fromYear, int toYear)
        {
            if (ori == null)
                return null;

            return FetchFBIObj($"{city_crime_endpoint}/{ori}/{fromYear}/{toYear}")?["results"];
        }

        public List<CityCrimeStats> CityCrimeRange(string city, State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchCityData(FetchORI(city, state.Abbrev), fromYear, toYear);

            if (results == null)
                return new();

            return Enumerable.Range(fromYear, toYear - fromYear)
                .Select(year => new CityCrimeStats(city, state, year, results)).ToList();
        }

        public List<BasicCrimeStats> CityCrimeRangeBasic(string city, State state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchCityData(FetchORI(city, state.Abbrev), fromYear, toYear);
            if (results == null)
                return new();

            return Enumerable.Range(fromYear, toYear - fromYear)
                .Select(year => new BasicCrimeStats(year, results)).ToList();
        }

        public List<CityCrimeStats> CityCrimeMulti(List<string> cities, State state, int? year = null)
        {
            var crimeYear = year ?? LatestYear;
            var oris = FetchORIs(cities, state.Abbrev);
            var results = new List<CityCrimeStats>();

            if (oris == null)
                return results;
            /*
            //this is LINQ that does the same thing. I personally think this is overkill
            var dumb = oris?.Select(ori => (ori.Item1, FetchCityData(ori.Item2, crimeYear, crimeYear)))
                .Where(data => data.Item2 != null)
                .Select(data => new CityCrimeStats(data.Item1, state, crimeYear, data.Item2));
            */
            foreach (var ori in oris)
            {
                var cityName = ori.Item1;
                var cityORI = ori.Item2;

                if (cityORI == null)
                    continue;

                var data = FetchCityData(cityORI, crimeYear, crimeYear);

                if (data == null)
                    continue;

                results.Add(new CityCrimeStats(cityName, state, crimeYear, data));

            }

            return results;
        }


        //National crime stats

        public List<NationalCrimeStats> NationalCrimeRange(int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{national_crime_endpoint}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return new();

            return results.Select(t => new NationalCrimeStats(t)).ToList();
        }

        public List<BasicCrimeStats> NationalCrimeRangeBasic(int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var results = FetchFBIObj($"{national_crime_endpoint}/{fromYear}/{toYear}")?["results"];

            if (results == null)
                return new();

            return results.Select(t => new BasicCrimeStats(t)).ToList();
        }


        //Misc.

        //Find all cities in a state
        public List<City>? CitiesIn(State state)
        {
            var agencies = FetchStateAgencies(state.Abbrev);

            if (agencies == null)
                return null;

            return agencies.Where(a => a["agency_type_name"]?.ToString() == "City").Select(a =>
            {
                var name = a["agency_name"]?.ToString() ?? "";
                var index = name.LastIndexOf(" Police Department");

                if (index != -1)
                    name = name.Substring(0, index);

                return new City {
                    Name = name,
                    ORI = a["ori"]?.ToString() ?? "NOTFOUND"
                };
            }).ToList();
        }

    }

}
