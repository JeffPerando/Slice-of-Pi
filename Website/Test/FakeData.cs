using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class FakeData
    {
        public static readonly List<StateCrimeSearchResult> CrimeResults = new()
        {
            new StateCrimeSearchResult { DateSearched = new DateTime(2020, 9, 2), State = "OR", Year = 2020, Population = 350000, Arson = 20, Assault = 15, Burglary = 2, Homicide = 59, Larceny = 0, MotorVehicleTheft = 9, PropertyCrimes = 42, RapeLegacy = 0, RapeRevised = 19, Robbery = 120, ViolentCrimes = 6 },
            new StateCrimeSearchResult { DateSearched = new DateTime(2020, 9, 2), State = "OR", Year = 2019, Population = 300000, Arson = 14, Assault = 9, Burglary = 33, Homicide = 6, Larceny = 13, MotorVehicleTheft = 40, PropertyCrimes = 6, RapeLegacy = 0, RapeRevised = 21, Robbery = 109, ViolentCrimes = 12 }
        };



    }

}
