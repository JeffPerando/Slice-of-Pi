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

namespace Test
{
    public class CS461FinalTest
    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void VerifySelectedRecomendationHasCorrectCityandState()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();
            //mocks user input
            string text = System.IO.File.ReadAllText(@"JObjectFile.txt");



            JObject crimeTrends = JObject.Parse(text);
            List<Crime> parsedDataList = new List<Crime>();
            //prepping to check if it is correct user input, i.e Scottsdale, Arizona
            parsedDataList = crimeRepo.ReturnTotalCityTrends(crimeTrends);


            //Assert that state is arizona, city is scottsdale

        }

        [Test]
        public void VerifyYear2020isPresent()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();
            //mocks user input
            string text = System.IO.File.ReadAllText(@"JObjectFile.txt");



            JObject crimeTrends = JObject.Parse(text);
            List<Crime> parsedDataList = new List<Crime>();
            //prepping to check if it is correct user input, i.e Scottsdale, Arizona
            parsedDataList = crimeRepo.ReturnTotalCityTrends(crimeTrends);


            //Assert that first year is 2020

        }
    }
}
