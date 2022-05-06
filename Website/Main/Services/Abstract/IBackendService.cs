
using Main.Models;
using Main.Models.FBI;

namespace Main.Services.Abstract
{
    public interface IBackendService
    {
        public List<State> GetAllStates();

        public State? StateFromAbbrev(string abbrev);

        public List<City> GetCitiesIn(int? stateID);

        public List<StateCrimeStats> CalcSafestStates();

    }

}
