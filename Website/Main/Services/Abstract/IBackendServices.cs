
using Main.Models;

namespace Main.Services.Abstract
{
    public interface IBackendServices
    {
        public List<string> GetAllStates();

        public List<Crime> CalcSafestStates();

    }

}
