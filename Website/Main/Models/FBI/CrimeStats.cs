
using Main.DAL.Concrete;
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class CrimeStats
    {
        public int Year { get; set; } = FBIService.LatestYear;

        public int TotalOffenses { get { return ViolentCrimes + PropertyCrimes; } }
        public int ViolentCrimes { get; set; }
        public int PropertyCrimes { get; set; }

        public int Assault { get; set; }
        public int Arson { get; set; }
        public int Burglary { get; set; }
        public int Homicide { get; set; }
        public int HumanTrafficking { get; set; }
        public int Larceny { get; set; }
        public int MotorVehicleTheft { get; set; }
        public int RapeLegacy { get; set; }
        public int RapeRevised { get; set; }
        public int Robbery { get; set; }

        public CrimeStats() {}

        public CrimeStats(JToken data)
        {
            Year = (int?)data["year"] ?? FBIService.LatestYear;

            ViolentCrimes = (int?)data["violent_crime"] ?? 0;
            PropertyCrimes = (int?)data["property_crime"] ?? 0;

            Arson = (int?)data["arson"] ?? 0;
            Assault = (int?)data["aggravated_assault"] ?? 0;
            Burglary = (int?)data["burglary"] ?? 0;
            Homicide = (int?)data["homicide"] ?? 0;
            Larceny = (int?)data["larceny"] ?? 0;
            MotorVehicleTheft = (int?)data["motor_vehicle_theft"] ?? 0;
            RapeLegacy = (int?)data["rape_legacy"] ?? 0;
            RapeRevised = (int?)data["rape_revised"] ?? 0;
            Robbery = (int?)data["robbery"] ?? 0;

        }

    }

}
