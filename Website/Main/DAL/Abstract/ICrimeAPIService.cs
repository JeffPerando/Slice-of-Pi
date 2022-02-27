using System;
using System.Collections.Generic;
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIService
    {
        void SetCredentials(string token);
        List<string>GetStates();
        List<Crime> GetSafestStates(List<Crime> states);
        List<Crime> GetCityStats(string cityName, string stateAbbrev);
        List<Crime> ReturnStateCrimeList(List<string> states);
        List<Crime> ReturnCityStats(List<Crime> city_stats);
<<<<<<< HEAD
        StateCrimeViewModel GetState( string stateAbbrev);
=======
        JObject GetCityTrends(string cityName, string stateAbbrev);
        public List<Crime> ReturnCityTrends(JObject city_stats);
>>>>>>> dev/dev
    }
        
}