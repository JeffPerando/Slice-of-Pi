
using Main.DAL.Abstract;
using Main.Models;
using Main.Services.Abstract;
using System.Diagnostics;

namespace Main.Services.Concrete
{
    public class HousePriceCalcService : IHousePriceCalcService
    {
        private readonly ICrimeAPIv2 _crime;
        private readonly IHousingAPI _housing;

        public HousePriceCalcService(ICrimeAPIv2 crime, IHousingAPI housing)
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

            var state = new State { Abbrev = addr.State };

            var stateCrimes = _crime.StateCrimeSingleBasic(state)?.TotalOffenses ?? 0;
            var cityCrimes = _crime.CityCrimeSingleBasic(addr.County, state)?.TotalOffenses ?? 0;
            var cityCount = _crime.CitiesIn(state)?.Count ?? 0;

            var stateCrimePerAgency = stateCrimes / (float)cityCount;

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
