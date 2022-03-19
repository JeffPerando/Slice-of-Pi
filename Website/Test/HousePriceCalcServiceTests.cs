
using NUnit.Framework;
using Main.DAL.Abstract;
using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class HousePriceCalcServiceTests
    {
        private static string state = "CA";
        private static string zip = "90210";
        private static string year = "2022";
        //private DummyCrimeAPI crimeAPI = new DummyCrimeAPI();
        //private IHousingAPI housingAPI = new DummyHousingAPI();
        //private IHousePriceCalcService priceCalc = new HousePriceCalcService(crimeAPI, housingAPI);
        private static Home address = new Home { StreetAddress = "1313 Mockingbird Ln.", County = "Hollywood", State = state, ZipCode = zip, Price = 500_000 };
        private static Dictionary<string, int> dummyCrimeData = new Dictionary<string, int> { { "murder", 4 } };

        [SetUp]
        public void Setup()
        {
            //add state crime and population
            //crimeAPI.SetPopulation(state, 20000);
            //crimeAPI.AddStateCrime(year, state, dummyCrimeData);

            //add hypothetical price to housing API (mirrored in Home struct [may remove that field])
            //housingAPI.AddAssessedValue(address, year, 500_000);

        }

        [TearDown]
        public void TearDown()
        {
            //crimeAPI.Clear();
            //housingAPI.Clear();
        }

        [Test]
        public void HousePriceCalcService_Calculate_HouseIsNeverWorthZero()
        {
            // Arrange
            // add a bunch of crime that will no doubt crash the home's price
            //crimeAPI.AddCrimes(year, zip, new { { "murder", Int32.MaxValue } });

            // Act
            //double estimate = priceCalc.Calculate(home, year);

            // Assert
            //Assert.That(estimate > 0);
        }

        [Test]
        public void HousePriceCalcService_Calculate_HousePriceIsCapped()
        {
            // Arrange
            // add NO CRIME, which should bring the home value up.

            // Act
            //double estimate = priceCalc.Calculate(home, year);

            // Assert
            //Assert.That(estimate < (home.Price * 5));
        }

        [Test]
        public void HousePriceCalcService_Calculate_BelowAvgCrimeBringsUpPrice()
        {
            // Arrange
            // add below-average crime (TBD)
            //crimeAPI.AddCrimes(year, zip, new { { "murder", 1 } });

            // Act
            //double estimate = priceCalc.Calculate(home, year);

            // Assert
            //Assert.That(estimate > home.Price);
        }

        [Test]
        public void HousePriceCalcService_Calculate_AverageCrimeKeepsPriceSteady()
        {
            //Arrange
            //add average crimes to current zip
            //crimeAPI.AddCrimes(year, zip, dummyCrimeData);

            //Act

            //double estimate = priceCalc.Calculate(home, year);

            // Assert
            //Assert.That(estimate == home.Price);
        }

        [Test]
        public void HousePriceCalcService_Calculate_CrimesAreWeighted()
        {
            //Arrange
            //add average crimes to current zip
            //crimeAPI.AddCrimes(year, zip, dummyCrimeData);

            //Act

            //double murderEst = priceCalc.Calculate(home, year);

            // add different, lesser crime than dummy data says
            // BUT keep the amount the same
            //crimeAPI.Clear();
            //crimeAPI.AddCrimes(year, zip, new { { "burglary", 4 } });

            //double burglaryEst = priceCalc.Calculate(home, year);

            // Assert
            //Assert.That(burglaryEst > murderEst);
        }

    }

}
