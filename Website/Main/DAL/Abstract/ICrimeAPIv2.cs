
using Main.DAL.Concrete;
using Main.Models;
using Main.Models.FBI;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIv2
    {
        /*
        With the new Crime API interface, the full potential of the FBI's Crime API is exposed. Sort of.

        All the custom methods have been made more generic. The rule of thumb is:

        If you just need crime stats for a given year, use a Single method like StateCrimeSingle
        If you need multiple crime stats, use a Multi method like StateCrimeMulti
        If you need crime stats over time, use a Range method like StateCrimeRange

        Finally, if you just need total convictions, add Basic to the end of the method name, i.e. CityCrimeRangeBasic
        */


        //State crime stats

        public StateCrimeStats? StateCrimeSingle(State state, int? year = null) => StateCrimeSingleAsync(state, year).GetAwaiter().GetResult();
        public Task<StateCrimeStats?> StateCrimeSingleAsync(State state, int? year = null);
        public List<StateCrimeStats?> StateCrimeMulti(List<State> states, int? year = null);
        public List<StateCrimeStats>? StateCrimeRange(State state, int fromYear, int toYear);

        public BasicCrimeStats? StateCrimeSingleBasic(State state, int? year = null) => StateCrimeRangeBasic(state, year ?? FBIService.LatestYear, year ?? FBIService.LatestYear)?[0];
        public List<BasicCrimeStats?> StateCrimeMultiBasic(List<State> states, int? year = null) => states.Select(state => StateCrimeSingleBasic(state, year)).ToList();
        public List<BasicCrimeStats>? StateCrimeRangeBasic(State state, int fromYear, int toYear);


        //City crime stats

        public CityCrimeStats? CityCrimeSingle(string city, State state, int? year = null) => CityCrimeRange(city, state, year ?? FBIService.LatestYear, year ?? FBIService.LatestYear)?[0];
        public List<CityCrimeStats?> CityCrimeMulti(List<string> cities, State state, int? year = null) => cities.Select(city => CityCrimeSingle(city, state, year)).ToList();
        public List<CityCrimeStats>? CityCrimeRange(string city, State state, int fromYear, int toYear);

        public BasicCrimeStats? CityCrimeSingleBasic(string city, State state, int? year = null) => CityCrimeRangeBasic(city, state, year ?? FBIService.LatestYear, year ?? FBIService.LatestYear)?[0];
        public List<BasicCrimeStats?> CityCrimeMultiBasic(List<string> cities, State state, int? year = null) => cities.Select(city => CityCrimeSingleBasic(city, state, year)).ToList();
        public List<BasicCrimeStats>? CityCrimeRangeBasic(string city, State state, int fromYear, int toYear);


        //National crime stats

        public NationalCrimeStats? NationalCrimeSingle(int? year = null) => NationalCrimeRange(year ?? FBIService.LatestYear, year ?? FBIService.LatestYear)?[0];
        public List<NationalCrimeStats>? NationalCrimeRange(int fromYear, int toYear);

        public BasicCrimeStats? NationalCrimeSingleBasic(int? year = null) => NationalCrimeRangeBasic(year ?? FBIService.LatestYear, year ?? FBIService.LatestYear)?[0];
        public List<BasicCrimeStats>? NationalCrimeRangeBasic(int fromYear, int toYear);


        //Lists the cities in a state
        public List<City>? CitiesIn(State state);

    }

}
