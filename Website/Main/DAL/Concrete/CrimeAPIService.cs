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

        public string state_json { get; }
        public string crime_api_state_info { get; }
        public string crime_statistics_api_url { get; }
        public string crime_url_agency_reported_crime { get; }

        public string crime_state_api_url { get; }



        public void SetCredentials(string token)
        {
            keyFBI = token;

        }
        public CrimeAPIService()
        {
            state_json = "https://gist.githubusercontent.com/mshafrir/2646763/raw/8b0dbb93521f5d6889502305335104218454c2bf/states_titlecase.json";
            crime_api_state_info = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states/";
            crime_statistics_api_url = "https://api.usa.gov/crime/fbi/sapi/api/agencies/byStateAbbr/";
            crime_url_agency_reported_crime = "https://api.usa.gov/crime/fbi/sapi/api/summarized/agencies/";

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

                    var jsonResponse = new WebClient().DownloadString(crime_api_state_info + states[i] + year.setYearForJSON(0) + keyFBI);
                    JObject info = JObject.Parse(jsonResponse);

                    float population = (int)info["results"][0]["population"];
                    float total_crime = (int)info["results"][0]["violent_crime"] + (int)info["results"][0]["property_crime"];
                    string state_abbrevs = (string)info["results"][0]["state_abbr"];

                    float crimes_per_capita = (float)Math.Round((total_crime / population) * 100000, 2);
                    string formatted_population = String.Format("{0:n0}", population);

                    states_crime.Add(new Crime { State = state_abbrevs, Population = formatted_population, Crime_Per_Capita = crimes_per_capita });


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
            var top_five_states = crimeList.OrderBy(c => c.Crime_Per_Capita).Take(5).ToList();

            return top_five_states;

        }

        public List<Crime> GetCityStatsByYear(string cityName, string stateAbbrev, string year)
        {
            List<Crime> city_crime_stats = new List<Crime>();

            var jsonResponse = new WebClient().DownloadString(crime_statistics_api_url + stateAbbrev + keyFBI);
            JObject info = JObject.Parse(jsonResponse);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {

                    var newjsonResponse = new WebClient().DownloadString(crime_url_agency_reported_crime + item["ori"] + "/offenses/" + year + "/" + year + "/" + keyFBI);
                    JObject city_stats = JObject.Parse(newjsonResponse);

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

            JSONYearVariable year = new JSONYearVariable();
            List<Crime> city_crime_stats = new List<Crime>();

            var jsonResponse = new WebClient().DownloadString(crime_statistics_api_url + stateAbbrev + keyFBI);
            JObject info = JObject.Parse(jsonResponse);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {

                    var newjsonResponse = new WebClient().DownloadString(crime_url_agency_reported_crime + item["ori"] + "/offenses" + year.setYearForJSON(0) + keyFBI);

                    JObject city_stats = JObject.Parse(newjsonResponse);

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


        public StateCrimeViewModel GetState(string stateAbbrev, int? aYear)
        {
            JSONYearVariable year = new JSONYearVariable();
            StateCrimeViewModel state_crime_stats = new StateCrimeViewModel();
            var jsonResponse = new WebClient().DownloadString(crime_api_state_info + stateAbbrev + year.setYearForJSON(aYear) + keyFBI);
            JObject info = JObject.Parse(jsonResponse);
            state_crime_stats = state_crime_stats.PresentJSONRespone(info);
            //var deserializedProduct = JsonConvert.DeserializeObject<IEnumerable<StateCrimeViewModel>>(jsonResponse);

            return state_crime_stats;
        }

        public JObject GetCityTrends(string cityName, string stateAbbrev)
        {
            JSONYearVariable year = new JSONYearVariable();
            List<Crime> city_crime_trends = new List<Crime>();
            var jsonResponse = new WebClient().DownloadString(crime_statistics_api_url + stateAbbrev + keyFBI);
            JObject info = JObject.Parse(jsonResponse);

            foreach (var item in info["results"])
            {
                var text = (string)item["agency_name"];
                var result = text.Contains(cityName + " " + "Police Department");

                //Checks to see if the city exists in the API.
                if (result)
                {
                    var newjsonResponse = new WebClient().DownloadString(crime_url_agency_reported_crime + item["ori"] + "/offenses" + "/" + (year.getYearTwoYearsAgo() - 35) + "/" + year.getYearTwoYearsAgo() + keyFBI);

                    JObject city_stats = JObject.Parse(newjsonResponse);

                    return city_stats;

                }

            }
            return null;
        }

        //FORMATS THE DATA INTO CRIME OBJECT LIST FOR GRAPH DISPLAYING
        public List<Crime> ReturnTotalCityTrends(JObject city_stats)
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
