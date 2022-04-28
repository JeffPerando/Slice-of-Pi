
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
            {
                return null;
            }

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
            {
                return null;
            }

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
                        cityORIs.Add(item["ori"]?.ToString());
                    }

                }

            }

            return null;
        }


        //State crime stats

        public async Task<StateCrimeStats?> StateCrimeSingleAsync(string state, int? year = null)
        {
            var data = await FetchFBIObjAsync($"{state_crime_url}/{state}/{year ?? LatestYear}/{year ?? LatestYear}");
            
            if (data == null)
            {
                return null;
            }

            return new StateCrimeStats(data);
        }

        public List<StateCrimeStats>? StateCrimeRange(string state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var data = FetchFBIObj($"{state_crime_url}/{state}/{fromYear}/{toYear}");

            if (data == null || data["results"] == null || !(data?["results"]?.Any() ?? false))
            {
                return null;
            }

            return data["results"]?.Select(t => new StateCrimeStats((JObject)t)).ToList();
        }

        public List<BasicCrimeStats>? StateCrimeRangeBasic(string state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var data = FetchFBIObj($"{state_crime_url}/{state}/{fromYear}/{toYear}");

            if (data == null || data["results"] == null || !(data?["results"]?.Any() ?? false))
            {
                return null;
            }

            return data["results"]?.Select(t => new BasicCrimeStats((JObject)t)).ToList();
        }


        //City crime stats

        //Let it be known that I will have nightmares about how the FBI reports its city crime stats
        //And the subtle differences between this API and the state one
        public List<CityCrimeStats>? CityCrimeRange(string city, string state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var ori = FetchCityORI(city, state);

            if (ori == null)
            {
                return null;
            }

            var data = FetchFBIObj($"{city_crime_url}/{ori}/{fromYear}/{toYear}");

            var results = data?["results"];

            if (results == null)
            {
                return null;
            }

            var crimes = new List<CityCrimeStats>();

            for (int year = fromYear; year <= toYear; year++)
            {
                crimes.Add(new CityCrimeStats(city, state, year, (JArray)results));

            }

            return crimes;
        }

        public List<BasicCrimeStats>? CityCrimeRangeBasic(string city, string state, int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var ori = FetchCityORI(city, state);

            if (ori == null)
            {
                return null;
            }

            var data = FetchFBIObj($"{city_crime_url}/{ori}/{fromYear}/{toYear}");

            var results = data?["results"];

            if (results == null)
            {
                return null;
            }

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

            var data = FetchFBIObj($"{national_crime_url}/{fromYear}/{toYear}");

            if (data?["results"] != null)
            {
                return data["results"]?.Select(t => new NationalCrimeStats((JObject)t)).ToList();
            }
            
            return null;
        }

        public List<BasicCrimeStats>? NationalCrimeRangeBasic(int fromYear, int toYear)
        {
            if (fromYear > toYear)
            {
                //swap them
                (fromYear, toYear) = (toYear, fromYear);
            }

            var data = FetchFBIObj($"{national_crime_url}/{fromYear}/{toYear}");

            if (data?["results"] != null)
            {
                return data["results"]?.Select(t => new BasicCrimeStats((JObject)t)).ToList();
            }

            return null;
        }


        //Misc.

        //Find all cities in a state
        public List<string>? CitiesIn(string state)
        {
            var agencies = FetchStateAgencies(state);

            if (agencies == null)
            {
                return null;
            }

            return agencies.Where(a => a["agency_type_name"]?.ToString() == "City").Select(a =>
            {
                var name = a["agency-name"]?.ToString() ?? "";

                return name.Substring(0, Math.Abs(name.LastIndexOf(" Police Department")));
            }).ToList();
        }

    }

}
