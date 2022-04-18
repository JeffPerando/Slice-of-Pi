
using Main.DAL.Abstract;
using Main.Models;
using Main.Services.Abstract;

namespace Main.Services.Concrete
{
    public class HousePriceCalcService : IHousePriceCalcService
    {
        private readonly ICrimeAPIService _crime;
        private readonly IHousingAPI _housing;

        public HousePriceCalcService(ICrimeAPIService crime, IHousingAPI housing)
        {
            _crime = crime;
            _housing = housing;

        }

        public HomeAssessment CalcCrimeWeightAssessment(Home address)
        {
            return new HomeAssessment
            {
                Address = address,
                InitAssessment = new DisplayPrice(40),
                WeightedAssessment = new DisplayPrice(600)
            };
        }

    }

}
