
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIService
    {
        //Supplanted by IBackendServices.GetAllStates()
        List<string> GetStates();
        //Supplanted by IBackendServices.CalcSafestStates()
        List<Crime> GetSafestStates(List<Crime> states);
        //Supplanted by ICrimeAPIv2.CityCrimeSingle()
        List<Crime> GetCityStats(string cityName, string stateAbbrev);
        //Supplanted by ICrimeAPIv2.StateCrimeMulti()
        List<Crime> ReturnStateCrimeList(List<string> states);
        //Supplanted by ICrimeAPIv2.StateCrimeSingleAsync()
        Task<Crime?> GetOverallStateCrimeAsync(string abbrev);
        //Supplanted by ICrimeAPIv2.StateCrimeSingle()
        StateCrimeSearchResult GetState(string stateAbbrev, int? aYear);
        //Supplanted by ICrimeAPIv2.CityCrimeSingle()
        List<Crime> GetCityStatsByYear(string cityName, string stateAbbrev, string year);

        //Supplanted by ICrimeAPIv2.CityCrimeRangeBasic()
        JObject GetCityTrends(string cityName, string stateAbbrev);
        List<Crime> ReturnTotalCityTrends(JObject city_stats);
        List<Crime> ReturnPropertyCityTrends(JObject city_stats);
        List<Crime> ReturnViolentCityTrends(JObject city_stats);

        //Supplanted by ICrimeAPIv2.CitiesIn()
        int GetCityCount(string state);
        //Supplanted by ICrimeAPIv2.CityCrimeSingle()
        int? GetTotalCityCrime(string city, string state);

    }

}