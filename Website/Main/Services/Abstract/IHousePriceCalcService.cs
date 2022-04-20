
using Main.Models;

namespace Main.Services.Abstract
{
    public interface IHousePriceCalcService
    {
        public WeightedAssessment CalcCrimeWeightAssessment(Home address);

    }

}
