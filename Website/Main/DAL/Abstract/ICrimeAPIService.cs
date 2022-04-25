//using System;
//using System.Collections.Generic;
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIService
    {
        List<string> GetStates();
        List<Crime> GetSafestStates(List<Crime> states);
        List<Crime> GetCityStats(string cityName, string stateAbbrev);
        List<Crime> ReturnStateCrimeList(List<string> states);
        Task<Crime?> GetOverallStateCrimeAsync(string abbrev);
        StateCrimeSearchResult GetState(string stateAbbrev, int? aYear);
        List<Crime> GetCityStatsByYear(string cityName, string stateAbbrev, string year);
        JObject GetCityTrends(string cityName, string stateAbbrev);
        List<Crime> ReturnTotalCityTrends(JObject city_stats);
        List<Crime> ReturnPropertyCityTrends(JObject city_stats);
        List<Crime> ReturnViolentCityTrends(JObject city_stats);
        int GetCityCount(string state);
        int? GetTotalCityCrime(string city, string state);

    }

}