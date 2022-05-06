
using Main.Models;
using Main.Models.FBI;

namespace Main.Services.Abstract
{
    public interface IBackendService
    {
        public List<State> GetAllStates();

        public State? StateFromAbbrev(string abbrev) => GetAllStates().Find(s => s.Abbrev == abbrev);

        public List<StateCrimeStats> CalcSafestStates();

    }

}
