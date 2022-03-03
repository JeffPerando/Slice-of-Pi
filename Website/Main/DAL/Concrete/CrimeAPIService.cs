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

        public string keyFBI = null;

        public string keyFBII = null;
        public string state_json { get; }
        public string crime_api_url { get; }
        public string crime_statistics_api_url { get; }
        public string crime_url_agency_reported_crime { get; }
        public string crime_state_api_url { get; }

        public void SetCredentials(string token)
        {
            keyFBI = token;
            keyFBII = token;
        }
        public CrimeAPIService()
        {
            state_json = "https://gist.githubusercontent.com/mshafrir/2646763/raw/8b0dbb93521f5d6889502305335104218454c2bf/states_titlecase.json";
            crime_api_url = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states/";
            crime_statistics_api_url = "https://api.usa.gov/crime/fbi/sapi/api/agencies/byStateAbbr/";
            crime_url_agency_reported_crime = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies/";
            crime_state_api_url = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states/";
        }

        public List<string> GetStates()
        {
            var jsonResponse = new WebClient().DownloadString(state_json);

            List<string> state_abbrevs = new List<string>();
            JArray info = JArray.Parse(jsonResponse);

            for (int i = 0; i < info.Count; i++)
            {
                string abbreviation = (string)info[i]["abbreviation"];
                state_abbrevs.Add(abbreviation);
            }
            return state_abbrevs;
        }

        public List<Crime> ReturnStateCrimeList(List<string> states)
        {
            JSONYearVariable year = new JSONYearVariable();
            List<Crime> states_crime = new List<Crime>();

            for (int i = 0; i < states.Count; i++)
            {
                try
                {
                    var jsonResponse = new WebClient().DownloadString(crime_api_url + states[i] + year.JSONVariableTwoYears + keyFBI);
                    JObject info = JObject.Parse(jsonResponse);

                    int crime_rate = (int)info["results"][0]["violent_crime"];
                    string state_abbrevs = (string)info["results"][0]["state_abbr"];

                    states_crime.Add(new Crime { State = state_abbrevs, ActualConvictions = crime_rate });
                }
                catch
                {
                    continue;
                }
            }

            return states_crime;
        }

        public List<Crime> GetSafestStates(List<Crime> crimeList)
        {
            var top_five_states = crimeList.OrderBy(a => a.ActualConvictions).Take(5).ToList();

            return top_five_states;

        }

        public List<Crime> GetCityStats(string cityName, string stateAbbrev)
        {

            JSONYearVariable year = new JSONYearVariable();
            List<Crime> city_crime_stats = new List<Crime>();
            string x = crime_statistics_api_url + stateAbbrev + keyFBI;

            Debug.WriteLine(x);
            var jsonResponse = new WebClient().DownloadString(crime_statistics_api_url + stateAbbrev + keyFBI);
            JObject info = JObject.Parse(jsonResponse);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {
                    var newjsonResponse = new WebClient().DownloadString(crime_url_agency_reported_crime + item["ori"] + "/offenses" + year.JSONVariableTwoYears + keyFBI);
                    JObject city_stats = JObject.Parse(newjsonResponse);

                    foreach (var crime in city_stats["results"])
                    {
                        if ((string)crime["offense"] == "property-crime")
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

        public stateCrimeViewModel GetState(string stateAbbrev)
        {
            JSONYearVariable year = new JSONYearVariable();
            stateCrimeViewModel state_crime_stats = new stateCrimeViewModel();
            var jsonResponse = new WebClient().DownloadString(crime_state_api_url + stateAbbrev + year.JSONVariableTwoYears + keyFBI);
            JObject info = JObject.Parse(jsonResponse);
            //var deserializedProduct = JsonConvert.DeserializeObject<IEnumerable<stateCrimeViewModel>>(jsonResponse);

            foreach (var item in info["results"])
            {
                try
                {
                    state_crime_stats.State_abbr = (string)item["state_abbr"];
                    state_crime_stats.Year = (int?)item["year"];
                    state_crime_stats.Population = (int?)item["population"];
                    state_crime_stats.Violent_crime = (int?)item["violent_crime"];
                    state_crime_stats.Homicide = (int)item["homicide"];
                    state_crime_stats.Rape_legacy = (int?)item["rape_legacy"];
                    state_crime_stats.Rape_revised = (int?)item["rape_revised"];
                    state_crime_stats.Robbery = (int?)item["robbery"];
                    state_crime_stats.Aggravated_assault = (int?)item["aggravated_assault"];
                    state_crime_stats.Property_crime = (int?)item["property_crime"];
                    state_crime_stats.Burglary = (int?)item["burglary"];
                    state_crime_stats.Larceny = (int?)item["larceny"];
                    state_crime_stats.Motor_vehicle_theft = (int?)item["motor_vehicle_theft"];
                    state_crime_stats.Arson = (int?)item["arson"];

                }
                catch
                {
                    continue;
                }
            }
            return state_crime_stats;
        }
    }
}







