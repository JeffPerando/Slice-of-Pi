using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Models;
using Microsoft.Extensions.Logging;
using Main.Controllers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

// THIS IS FOR THE FINAL EXAM, LINK TO MY JIRA PAGE BELOW
// https://sliceofpi.atlassian.net/browse/SP-102?atlOrigin=eyJpIjoiMGI1MDU1YTQxYjBmNDgzMTlhOTIwM2FmNWNkNzQ5ZTMiLCJwIjoiaiJ9

namespace Final_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }
        
        [Test]
        public void Verify_Years_Within_DropDown()
        {
            //Arrange
            ICrimeAPIService crimeRepo = new CrimeAPIService();
            List<Crime> checkYears = new List<Crime>();
            string years = System.IO.File.ReadAllText(@"JObjectFile.txt");

            //Act
            JObject yearsReported = JObject.Parse(years);
            checkYears = crimeRepo.ReturnYearsDropdown(yearsReported);

            //Assert
            Assert.IsTrue(checkYears.Any(y => y.Years == 2018));
            Assert.IsTrue(checkYears.Any(y => y.Years == 2019));
            Assert.IsTrue(checkYears.Any(y => y.Years == 2020));

            //If true all of the years will be properly sent to the JavaScript (Disregards order)!
        }

        [Test]
        public void Verify_Years_Order_Descending()
        {
            //Arrange
            ICrimeAPIService crimeRepo = new CrimeAPIService();
            List<Crime> checkYears = new List<Crime>();
            string years = System.IO.File.ReadAllText(@"JObjectFile.txt");

            //Act
            JObject yearsReported = JObject.Parse(years);
            checkYears = crimeRepo.ReturnYearsDropdown(yearsReported);

            //Assert
            Assert.AreEqual(checkYears[0].Years, 2020);
            Assert.AreEqual(checkYears[1].Years, 2019);
            Assert.AreEqual(checkYears[2].Years, 2018);

        }

        [Test]
        public void Verify_Year_Amount()
        {
            //Arrange
            ICrimeAPIService crimeRepo = new CrimeAPIService();
            List<Crime> checkYears = new List<Crime>();
            string years = System.IO.File.ReadAllText(@"JObjectFile.txt");

            //Act
            JObject yearsReported = JObject.Parse(years);
            checkYears = crimeRepo.ReturnYearsDropdown(yearsReported);

            //Assert
            Assert.AreEqual(checkYears.Count(), 3);
        }

        [Test]
        public void Verify_Information_For_Year_Given()
        {
            //Arrange
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            string text = System.IO.File.ReadAllText(@"JObjectFile.txt");

            JObject crimeTrends = JObject.Parse(text);
            List<Crime> parsedDataList = new List<Crime>();
            //Gets crimes for 2018 - 2020 meaning order should be 2018 to 2020.
            parsedDataList = crimeRepo.ReturnViolentCityTrends(crimeTrends);
            TestContext.WriteLine(parsedDataList[0].TotalOffenses);
            TestContext.WriteLine(parsedDataList[1].TotalOffenses);
            TestContext.WriteLine(parsedDataList[2].TotalOffenses);

            //Assert (GETS MULTIPLE YEARS PROPERTY CRIMES FOR SAID YEAR)
            Assert.IsTrue(parsedDataList[0].TotalOffenses == 22 && parsedDataList[0].Year == 2018);
            Assert.IsTrue(parsedDataList[1].TotalOffenses == 26 && parsedDataList[1].Year == 2019);
            Assert.IsTrue(parsedDataList[2].TotalOffenses ==  17 && parsedDataList[2].Year == 2020);
        }
    }
}
