
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

        public BackendService(ICrimeAPIv2 crime)
        {
            _crime = crime;

        }

        public List<StateCrimeStats> CalcSafestStates()
        {
            var nationalStats = _crime.StateCrimeMulti(State.AllStates);
            
            return nationalStats.OrderBy(c => c.CrimePerCapita).Take(5).ToList();
        }

    }

}
