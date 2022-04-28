
using Main.Models;
using Main.Models.FBI;

namespace Main.Services.Abstract
{
    public interface IBackendService
    {
        public List<string> GetAllStates();

        public List<StateCrimeStats?> CalcSafestStates();

    }

}
