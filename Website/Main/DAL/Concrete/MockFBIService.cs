
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.FBI;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class MockFBIService : ICrimeAPIv2
    {
        public MockFBIService() {}

        public List<City>? CitiesIn(State state)
        {
            throw new NotImplementedException();
        }

        public List<CityCrimeStats> CityCrimeMulti(List<string> cities, State state, int? year = null)
        {
            throw new NotImplementedException();
        }

        public List<CityCrimeStats> CityCrimeRange(string city, State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public List<BasicCityStats> CityCrimeRangeBasic(string city, State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public List<NationalCrimeStats> NationalCrimeRange(int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public List<BasicCrimeStats> NationalCrimeRangeBasic(int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public List<StateCrimeStats> StateCrimeMulti(List<State> states, int? year = null)
        {
            throw new NotImplementedException();
        }

        public List<StateCrimeStats> StateCrimeRange(State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public List<BasicCrimeStats> StateCrimeRangeBasic(State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public Task<StateCrimeStats?> StateCrimeSingleAsync(State state, int? year = null)
        {
            throw new NotImplementedException();
        }
    }

}
