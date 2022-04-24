
using Main.DAL.Abstract;
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class MockCrimeAPIService : ICrimeAPIService
    {
        public MockCrimeAPIService() {}

        public virtual int GetCityCount(string state)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> GetCityStats(string cityName, string stateAbbrev)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> GetCityStatsByYear(string cityName, string stateAbbrev, string year)
        {
            throw new NotImplementedException();
        }

        public virtual JObject GetCityTrends(string cityName, string stateAbbrev)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Crime?> GetOverallStateCrimeAsync(string abbrev)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> GetSafestStates(List<Crime> states)
        {
            throw new NotImplementedException();
        }

        public virtual StateCrimeSearchResult GetState(string stateAbbrev, int? aYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<string> GetStates()
        {
            throw new NotImplementedException();
        }

        public virtual int? GetTotalCityCrime(string city, string state)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> ReturnPropertyCityTrends(JObject city_stats)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> ReturnStateCrimeList(List<string> states)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> ReturnTotalCityTrends(JObject city_stats)
        {
            throw new NotImplementedException();
        }

        public virtual List<Crime> ReturnViolentCityTrends(JObject city_stats)
        {
            throw new NotImplementedException();
        }

    }

}
