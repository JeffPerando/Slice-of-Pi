
using Main.Models;

namespace Main.Services.Abstract
{
    public interface IBackendService
    {
        public List<string> GetAllStates();

        public List<Crime> CalcSafestStates();

    }

}
