
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Helpers;
using Main.Models;
using Main.Models.FBI;
using Main.Services.Abstract;

namespace Main.Services.Concrete
{
    public class BackendService : IBackendService
    {
        private const int CaliforniaID = 4;

        private readonly ICrimeAPIv2 _crime;
        private readonly List<State> states;

        public BackendService(ICrimeAPIv2 crime)
        {
            _crime = crime;

            states = FileHelper.ReadInto<List<State>>("states.json") ?? new();

        }

        public List<State> GetAllStates()
        {
            return states;
        }

        public State? StateFromAbbrev(string abbrev)
        {
            if (abbrev.Length != 2)
            {
                return null;
            }

            return states.Find(s => s.Abbrev == abbrev);
        }

        public List<City> GetCitiesIn(int? stateID)
        {
            return _crime.CitiesIn(states[stateID ?? CaliforniaID]) ?? new();
        }

        public List<StateCrimeStats> CalcSafestStates()
        {
            var nationalStats = _crime.StateCrimeMulti(GetAllStates());
            
            return nationalStats.OrderBy(c => c.CrimePerCapita).Take(5).ToList();
        }

        public object? GetCityTrends(string? city, State state)
        {
            if (city == null)
            {
                city = "Riverside";
            }

            var stats = _crime.CityCrimeRangeBasic(city, state, FBIService.OldestYear, FBIService.LatestYear);

            if (stats == null)
                return null;

            return new
            {
                totalTrends = stats,
                propertyTrends = stats.Select(s => new { Year = s.Year, TotalOffenses = s.PropertyCrimes, OffenseType = "property-crimes" }),
                violentTrends = stats.Select(s => new { Year = s.Year, TotalOffenses = s.ViolentCrimes, OffenseType = "violent-crimes" })
            };
        }

    }

}
