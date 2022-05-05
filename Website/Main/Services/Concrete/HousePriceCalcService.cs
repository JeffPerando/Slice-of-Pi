
using Main.DAL.Abstract;
using Main.Models;
using Main.Services.Abstract;
using System.Diagnostics;

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

        public WeightedAssessment CalcCrimeWeightAssessment(Home addr)
        {
            WeightedAssessment asm = new();

            asm.Address = addr;

            var price = _housing.GetAssessmentFor(addr);
            
            if (price == null)
            {
                return asm;
            }

            var homePrice = price.MarketValue;
            
            if (homePrice == 0)
            {
                Debug.WriteLine($"Oh look, free housing: {addr.StreetAddress}, {addr.StreetAddress2}");
                return asm;
            }

            asm.InitAssessment = new DisplayPrice(homePrice);

            var stateCrime = _crime.GetOverallStateCrimeAsync(addr.State).GetAwaiter().GetResult();
            var cityCrimes = _crime.GetTotalCityCrime(addr.County, addr.State) ?? 0;
            var cityCount = _crime.GetCityCount(addr.State);

            var stateCrimePerAgency = (stateCrime?.TotalOffenses ?? 0) / (float)cityCount;

            //the more city crimes, the lower this percentage. which translates to a lower home price
            var cityCrimePercentage = stateCrimePerAgency / cityCrimes;
            var priceWeight = Math.Clamp(cityCrimePercentage, 0.55f, 1.2f);

            var weightedPrice = 0f + homePrice;

            var currentYear = DateTime.UtcNow.Year;

            //If the tax year is 2 years older, we account for inflation (~3.7% a year)
            //Could make it TaxYear < CurrentYear, but taxes tend to lag behind a bit.
            if (price.TaxYear < currentYear - 1)
            {
                weightedPrice *= (float)Math.Pow(1.037, currentYear - price.TaxYear);
            }

            weightedPrice *= priceWeight;

            asm.CalcAssessment = new DisplayPrice(weightedPrice);

            return asm;

        }

    }

}
