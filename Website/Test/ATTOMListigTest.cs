using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Models;
using Main.Models.Listings;
using Microsoft.Extensions.Logging;
using Main.Controllers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;


namespace Test
{
    public class ATTOMTests

    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void VerifyOrderCrimePerCapita()
        {
            AttomJson Listings = new AttomJson();

            var Address = new List<Address>()
            {
                new Address { oneLine = "1986 tesla rd nw 97304 spacex tx"  },
                new Address { oneLine = "1738 badger blvd sw 19002 nasa oh" },
                new Address { oneLine = "1337 leet st w 97304 legend va"    },
                new Address { oneLine = "0223  stag rd e 42069 moon ca"     },
                new Address { oneLine = "0308  rancho rd nw 76251 campo az" }
            };



            var expectedListings = new List<Property>()
            {
                new Property { address}
            };

            var randomList = new List<Crime>      // Sorts the highest actual conviction first. 
            {
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime", CrimePerCapita = 1552.6f },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny", CrimePerCapita = 1622.6f },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary", CrimePerCapita = 1582.6f },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide", CrimePerCapita = 2672.6f },
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson", CrimePerCapita = 1512.6f },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape", CrimePerCapita = 195.2f }
            };

            var expectedList = new List<Crime>
            {
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson", CrimePerCapita = 195.2f },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape", CrimePerCapita = 1512.6f },
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime", CrimePerCapita = 1552.6f },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary", CrimePerCapita = 1582.6f },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide" , CrimePerCapita = 1622.6f },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny", CrimePerCapita = 2672.6f }
            };

            List<Crime> theList = new List<Crime>();
 

            Assert.AreEqual(theList[0].CrimePerCapita, expectedList[0].CrimePerCapita);
            Assert.AreEqual(theList[1].CrimePerCapita, expectedList[1].CrimePerCapita);
            Assert.AreEqual(theList[2].CrimePerCapita, expectedList[2].CrimePerCapita);
            Assert.AreEqual(theList[3].CrimePerCapita, expectedList[3].CrimePerCapita);
            Assert.AreEqual(theList[4].CrimePerCapita, expectedList[4].CrimePerCapita);
        }

    }
}

