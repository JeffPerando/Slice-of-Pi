
using Main.Models;
using Main.Models.FBI;

namespace Main.Services.Abstract
{
    public interface IBackendService
    {
        public List<State> GetAllStates();

        public List<StateCrimeStats?> CalcSafestStates();

        public object? GetCityTrends(string? city, int? state);

    }

}
