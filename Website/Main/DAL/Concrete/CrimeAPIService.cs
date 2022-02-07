using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class CrimeAPIService : ICrimeAPIService
    {

        public string keyFBI = null;
        public string state_json {get;}
        public string crime_api_url {get;}

        public void SetCredentials(string token)
        {
            keyFBI = token;
        }
        public CrimeAPIService()
        {
            state_json = "https://gist.githubusercontent.com/mshafrir/2646763/raw/8b0dbb93521f5d6889502305335104218454c2bf/states_titlecase.json";
            crime_api_url = "https://api.usa.gov/crime/fbi/sapi/api/estimates/states/";
        }


        public List<string>GetStates()
        {
            var jsonResponse = new WebClient().DownloadString(state_json);

            List<string> state_abbrevs = new List<string>();
            JArray info = JArray.Parse(jsonResponse);

            for ( int i = 0; i < info.Count; i++)
            {
                string abbreviation = (string)info[i]["abbreviation"];
                state_abbrevs.Add(abbreviation);

            }
            
            return state_abbrevs;
        }

        public List<Crime> GetSafestStates(List<string> states)
        {
            var two_years_ago = (DateTime.Now.Year - 2).ToString();
            string two_year_link = '/' + two_years_ago + '/' + two_years_ago;
            List<Crime> states_crime = new List<Crime>();
            
            for ( int i = 0; i < states.Count; i++ )
            {
                try
                {
                    var jsonResponse = new WebClient().DownloadString(crime_api_url + states[i] + two_year_link + keyFBI);
                    JObject info = JObject.Parse(jsonResponse);
                    
                    int crime_rate = (int)info["results"][0]["violent_crime"];
                    string state_abbrevs = (string)info["results"][0]["state_abbr"];

                    states_crime.Add(new Crime {State = state_abbrevs, ActualConvictions = crime_rate});
                }
                catch
                {
                    Debug.WriteLine("\""+ states[i] + "\" is not avaiable!");
                }
            }
            
            var top_five_states = states_crime.OrderBy(a => a.ActualConvictions).ToList();


            if (top_five_states.Count > 5)
            {
                top_five_states.RemoveRange(5, top_five_states.Count - 5);
            }

            return top_five_states;

        }
    }
}