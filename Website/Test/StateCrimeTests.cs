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
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace NUnit_Test
{
    public class StateCrimeTests
    {
        [SetUp]
        public void Setup()
        {


        }


        [Test]
        public void VerifyViewModelIsProperlyPopulated()
        {
            //Arrange
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            string text = System.IO.File.ReadAllText(@"StateCrimeJobject.txt");



            JObject crimeStats = JObject.Parse(text);
            StateCrimeViewModel stateCrime = new StateCrimeViewModel();
            //the text in the file is in jobject form,
            stateCrime = stateCrime.PresentJSONRespone(crimeStats);




    //Assert that the text is parsed correctly by checking if view model variables are correct and prove we can use JSON data
    //note the json in the file was pulled from the API
            Assert.AreEqual(stateCrime.Year, 2020);
            Assert.AreEqual(stateCrime.State_abbr, "AL");
            Assert.AreEqual(stateCrime.Population, 4921532);
            Assert.AreEqual(stateCrime.Violent_crime, 22322);
            Assert.AreEqual(stateCrime.Homicide, 471);
            Assert.AreEqual(stateCrime.Rape_legacy, 0);
            Assert.AreEqual(stateCrime.Rape_revised, 1608);
            Assert.AreEqual(stateCrime.Robbery, 2666);
            Assert.AreEqual(stateCrime.Aggravated_assault, 17577);
            Assert.AreEqual(stateCrime.Property_crime, 105161);
            Assert.AreEqual(stateCrime.Burglary, 19660);
            Assert.AreEqual(stateCrime.Larceny, 74575);
            Assert.AreEqual(stateCrime.Motor_vehicle_theft, 10926);
            Assert.AreEqual(stateCrime.Arson, 34);

        }
    }
}

