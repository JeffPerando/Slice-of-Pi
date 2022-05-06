
using NUnit.Framework;
using Main.DAL.Abstract;
using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Main.DAL.Concrete;
using Main.Services.Concrete;
using Main.Models.FBI;

namespace Test
{
    public class HousePriceCalcServiceTests
    {
        private static readonly Home address = new() { StreetAddress = "1313 Mockingbird Ln.", County = "Hollywood", State = "CA", ZipCode = "90210" };

        private static ICrimeAPIv2 MockCrimeAPI(int stateCrimes, int cityCrimes, int cityCount)
        {
            var mock = new Mock<MockFBIService>();
            mock.CallBase = true;

            mock.Setup(e => e.CitiesIn(It.IsAny<State>()))
                .Returns(Enumerable.Range(0, cityCount).Select(i => new City { Name = "" + i }).ToList());

            mock.Setup(e => e.StateCrimeRangeBasic(It.IsAny<State>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<BasicCrimeStats>()
                {
                    new BasicCrimeStats { PropertyCrimes = stateCrimes }
                });

            mock.Setup(e => e.CityCrimeRangeBasic(It.IsAny<string>(), It.IsAny<State>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<BasicCityStats>()
                {
                    new BasicCityStats { PropertyCrimes = cityCrimes }
                });

            //mock.Setup(api => api.GetOverallStateCrimeAsync(It.IsAny<string>())).ReturnsAsync(new Crime { TotalOffenses = stateCrimes });
            //mock.Setup(api => api.GetTotalCityCrime(It.IsAny<string>(), It.IsAny<string>())).Returns(cityCrimes);
            //mock.Setup(api => api.GetCityCount(It.IsAny<string>())).Returns(cityCount);

            return mock.Object;
        }

        private static IHousingAPI MockHousingAPI(HomeAssessment assess)
        {
            var mock = new Mock<MockATTOMService>();
            mock.CallBase = true;
            mock.Setup(api => api.GetAssessmentFor(It.IsAny<Home>())).Returns(assess);

            return mock.Object;
        }

        [SetUp]
        public void Setup() {}

        [TearDown]
        public void TearDown() {}

        [Test]
        public void HousePriceCalcService_Calculate_HouseIsNeverWorthZero()
        {
            // Arrange
            //cityCrimes should be higher than stateCrimes / cityCount
            //the point is, no matter how stupidly high the crime rate is, a property won't be valuated at 0
            var crime = MockCrimeAPI(
                stateCrimes: Int16.MaxValue,
                cityCrimes: Int32.MaxValue,
                cityCount: 2);

            var prices = new HomeAssessment
            {
                AssessedValue = 200_000,
                MarketValue = 300_000,
                TaxYear = 2021
            };

            var housing = MockHousingAPI(prices);

            var calc = new HousePriceCalcService(crime, housing);

            // Act
            var wghtAssess = calc.CalcCrimeWeightAssessment(address);

            // Assert
            Assert.That(wghtAssess.CalcAssessment > new DisplayPrice(0));
        }

        [Test]
        public void HousePriceCalcService_Calculate_HousePriceIsCapped()
        {
            // Arrange
            // add NO CITY CRIME, which should bring the home value up.
            var crime = MockCrimeAPI(
                stateCrimes: 255,
                cityCrimes: 0,
                cityCount: 2);

            var prices = new HomeAssessment
            {
                AssessedValue = 200_000,
                MarketValue = 300_000,
                TaxYear = 2021
            };

            var housing = MockHousingAPI(prices);

            var calc = new HousePriceCalcService(crime, housing);

            // Act
            var wghtAssess = calc.CalcCrimeWeightAssessment(address);

            // Assert
            Assert.That(wghtAssess.CalcAssessment < new DisplayPrice(prices.MarketValue * 2));

        }

        [Test]
        public void HousePriceCalcService_Calculate_BelowAvgCrimeBringsUpPrice()
        {
            // Arrange
            // add below-average crime
            // in this case, 256 / 2 = 128, which is less than 64. hence, lower than average
            var crime = MockCrimeAPI(
                stateCrimes: 256,
                cityCrimes: 64,
                cityCount: 2);

            var prices = new HomeAssessment
            {
                AssessedValue = 200_000,
                MarketValue = 300_000,
                TaxYear = 2021
            };

            var housing = MockHousingAPI(prices);

            var calc = new HousePriceCalcService(crime, housing);

            // Act
            var wghtAssess = calc.CalcCrimeWeightAssessment(address);

            // Assert
            Assert.That(wghtAssess.CalcAssessment > new DisplayPrice(prices.MarketValue));

        }

        [Test]
        public void HousePriceCalcService_Calculate_AverageCrimeKeepsPriceSteady()
        {
            // Arrange
            // add average crime
            var crime = MockCrimeAPI(
                stateCrimes: 256,
                cityCrimes: 128,
                cityCount: 2);

            var prices = new HomeAssessment
            {
                AssessedValue = 200_000,
                MarketValue = 300_000,
                TaxYear = 2021
            };

            var housing = MockHousingAPI(prices);

            var calc = new HousePriceCalcService(crime, housing);

            // Act
            var wghtAssess = calc.CalcCrimeWeightAssessment(address);

            // Assert
            Assert.That(wghtAssess.CalcAssessment == new DisplayPrice(prices.MarketValue));

        }

        [Test]
        public void HousePriceCalcService_Calculate_InflationIsIncluded()
        {
            // Arrange
            var crime = MockCrimeAPI(
                stateCrimes: 256,
                cityCrimes: 128,
                cityCount: 2);

            var prices = new HomeAssessment
            {
                AssessedValue = 200_000,
                MarketValue = 300_000,
                //Note the old tax year
                TaxYear = 2018
            };

            var housing = MockHousingAPI(prices);

            var calc = new HousePriceCalcService(crime, housing);

            // Act
            var wghtAssess = calc.CalcCrimeWeightAssessment(address);

            // Assert
            Assert.That(wghtAssess.CalcAssessment > new DisplayPrice(prices.MarketValue));

        }

    }

}
