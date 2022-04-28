
using Main.DAL.Abstract;
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

        public List<string> GetAllStates()
        {
            return states.Select(s => s.Abbrev).ToList();
        }

        public List<StateCrimeStats?> CalcSafestStates()
        {
            var nationalStats = _crime.StateCrimeMulti(GetAllStates());
            
            return nationalStats.OrderBy(c => c?.CrimePerCapita ?? 0).Take(5).ToList();
        }

    }

}
