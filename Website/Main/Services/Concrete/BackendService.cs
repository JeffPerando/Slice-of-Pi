
using Main.DAL.Abstract;
using Main.Helpers;
using Main.Models;
using Main.Services.Abstract;

namespace Main.Services.Concrete
{
    public class BackendService : IBackendService
    {
        private readonly ICrimeAPIService _crime;

        private readonly List<State> states;

        public BackendService(ICrimeAPIService crime)
        {
            _crime = crime;

            states = FileHelper.ReadInto<List<State>>("states.json") ?? new();

        }

        public List<string> GetAllStates()
        {
            return states.Select(s => s.Abbrev).ToList();
        }

        public List<Crime> CalcSafestStates()
        {
            var state_list = GetAllStates();
            var get_national_stats = _crime.ReturnStateCrimeList(state_list);
            var top_five_states = _crime.GetSafestStates(get_national_stats);

            return top_five_states;
        }

    }

}
