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

namespace NUnit_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void VerifyOrder()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            var expectedList = new List<Crime>      // now sorted correctly
            {
                new Crime { State = "AB", ActualConvictions = 1738   },
                new Crime { State = "CD", ActualConvictions = 69     },
                new Crime { State = "EF", ActualConvictions = 420    },
                new Crime { State = "GH", ActualConvictions = 1337   },
                new Crime { State = "IJ", ActualConvictions = 24     },
                new Crime { State = "KL", ActualConvictions = 25     }
            };

            var compareList = new List<Crime>      // now sorted correctly
            {
                new Crime { State = "IJ", ActualConvictions = 24   },
                new Crime { State = "KL", ActualConvictions = 25   },
                new Crime { State = "CD", ActualConvictions = 69   },
                new Crime { State = "EF", ActualConvictions = 420  },
                new Crime { State = "GH", ActualConvictions = 1337 }
            };

            List<Crime> theList = new List<Crime>();
            theList = crimeRepo.GetSafestStates(expectedList);

            Assert.AreEqual(theList[0].State, compareList[0].State);
            Assert.AreEqual(theList[1].State, compareList[1].State);
            Assert.AreEqual(theList[2].State, compareList[2].State);
            Assert.AreEqual(theList[3].State, compareList[3].State);
            Assert.AreEqual(theList[4].State, compareList[4].State);
        }

        [Test]
        public void VerifyAmountCutOff()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            var expectedList = new List<Crime>      // now sorted correctly
            {
                new Crime { State = "AB", ActualConvictions = 1738   },
                new Crime { State = "CD", ActualConvictions = 69     },
                new Crime { State = "EF", ActualConvictions = 420    },
                new Crime { State = "GH", ActualConvictions = 1337   },
                new Crime { State = "IJ", ActualConvictions = 24     },
                new Crime { State = "KL", ActualConvictions = 25     }
            };

            List<Crime> theList = new List<Crime>();
            theList = crimeRepo.GetSafestStates(expectedList);

            Assert.AreEqual(theList.Count, 5);

        }

        [Test]
        public void VerifyOrderOfCrimesAmounts()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            var randomList = new List<Crime>      // Sorts the highest actual conviction first. 
            {
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime" },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny"        },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary"       },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide"       },
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson"          },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape"           }
            };

            var compareList = new List<Crime>     
            {
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime" },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide"       },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary"       },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny"        },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape"           },
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson"          }
            };

            List<Crime> theList = new List<Crime>();
            theList = crimeRepo.ReturnCityStats(randomList);

            Assert.AreEqual(theList[0].TotalOffenses, compareList[0].TotalOffenses);
            Assert.AreEqual(theList[1].TotalOffenses, compareList[1].TotalOffenses);
            Assert.AreEqual(theList[2].TotalOffenses, compareList[2].TotalOffenses);
            Assert.AreEqual(theList[3].TotalOffenses, compareList[3].TotalOffenses);
            Assert.AreEqual(theList[4].TotalOffenses, compareList[4].TotalOffenses);
        }

        [Test]
        public void VerifyCrimeTypeCount()
        {
            ICrimeAPIService crimeRepo = new CrimeAPIService();

            var randomList = new List<Crime>      // Sorts the highest actual conviction first. 
            {
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime" },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny"        },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary"       },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide"       },
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson"          },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape"           }
            };

            var compareList = new List<Crime>
            {
                new Crime { State = "AB", TotalOffenses = 1738, OffenseType = "property-crime" },
                new Crime { State = "GH", TotalOffenses = 1337, OffenseType = "homicide"       },
                new Crime { State = "EF", TotalOffenses = 420 , OffenseType = "burglary"       },
                new Crime { State = "CD", TotalOffenses = 69  , OffenseType = "larceny"        },
                new Crime { State = "KL", TotalOffenses = 25  , OffenseType = "rape"           },
                new Crime { State = "IJ", TotalOffenses = 24  , OffenseType = "arson"          }
            };

            List<Crime> theList = new List<Crime>();
            theList = crimeRepo.ReturnCityStats(randomList);

            Assert.AreEqual(theList.Count, 6);
        }
    }
}










//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Main.DAL.Abstract;
//using Main.DAL.Concrete;
//using Main.Models;
//using Microsoft.Extensions.Logging;
//using Main.Controllers;
//using Microsoft.Extensions.Configuration;

//namespace NUnit_Test
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        private readonly ILogger<HomeController> _logger;
//        private readonly ICrimeAPIService _CrimeService;
//        private readonly IConfiguration _config;

//    }
//    [Test]
//    public void Test1()
//    {
//        List<string> state_list = new List<string>();
//        List<Crime> top_five_states = new List<Crime>();

//        _CrimeService.SetCredentials(_config["apiFBIKey"]);
//        state_list = _CrimeService.GetStates();
//        top_five_states = _CrimeService.GetSafestStates(state_list);
//        return Json(top_five_states);
//    }
//}
//}