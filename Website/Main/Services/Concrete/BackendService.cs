
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

        public List<StateCrimeStats?> CalcSafestStates()
        {
            var nationalStats = _crime.StateCrimeMulti(GetAllStates());
            
            return nationalStats.OrderBy(c => c?.CrimePerCapita ?? 0).Take(5).ToList();
        }

        public object? GetCityTrends(string? city, int? stateID)
        {
            if (city == null || stateID == null)
            {
                city = "Riverside";
                // 4 is CA
                stateID = 4;
            }

            State state = states[(int)stateID];

            var stats = _crime.CityCrimeRangeBasic(city, state, FBIService.OldestYear, FBIService.LatestYear);

            if (stats == null)
                return null;

            return new
            {
                totalTrends = stats,
                propertyTrends = stats.Select(s => new { year = s.Year, propertyCrimes = s.PropertyCrimes }),
                violentTrends = stats.Select(s => new { year = s.Year, violentCrimes = s.ViolentCrimes })
            };
        }

    }

}
