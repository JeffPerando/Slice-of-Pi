using Main.Helpers;
using Main.Services.Abstract;
using Main.Services.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CSVBuildTests
    {
        [SetUp]
        public void Setup() {}

        [TearDown]
        public void TearDown() {}

        [Test]
        public void CSVBuilder_CanBuildCSV()
        {
            
            var csv = CSVParseHelper.fromStateSearchHistory(FakeData.CrimeResults);
            Assert.AreEqual(csv,
                @$"DateSearched,State,Year,Population,ViolentCrimes,Homicide,RapeLegacy,RapeRevised,Robbery,Assault,PropertyCrimes,Burglary,Larceny,MotorVehicleTheft,Arson
{new DateTime(2020, 9, 2).ToString("MM/dd/yyyy HH:mm:ss")},OR,2020,350000,6,59,0,19,120,15,42,2,0,9,20
{new DateTime(2020, 9, 2).ToString("MM/dd/yyyy HH:mm:ss")},OR,2019,300000,12,6,0,21,109,9,6,33,13,40,14
");
            
            Assert.True(true);

        }

    }

}
