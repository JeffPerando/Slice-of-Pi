
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.FBI;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class MockFBIService : ICrimeAPIv2
    {
        public MockFBIService() {}

        public virtual List<string>? CitiesIn(State state)
        {
            throw new NotImplementedException();
        }

        public virtual List<CityCrimeStats> CityCrimeMulti(List<string> cities, State state, int? year = null)
        {
            throw new NotImplementedException();
        }

        public virtual List<CityCrimeStats> CityCrimeRange(string city, State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<BasicCityStats> CityCrimeRangeBasic(string city, State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<NationalCrimeStats> NationalCrimeRange(int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<BasicCrimeStats> NationalCrimeRangeBasic(int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<StateCrimeStats> StateCrimeMulti(List<State> states, int? year = null)
        {
            throw new NotImplementedException();
        }

        public virtual List<StateCrimeStats> StateCrimeRange(State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual List<BasicCrimeStats> StateCrimeRangeBasic(State state, int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public virtual Task<StateCrimeStats?> StateCrimeSingleAsync(State state, int? year = null)
        {
            throw new NotImplementedException();
        }

    }

}
