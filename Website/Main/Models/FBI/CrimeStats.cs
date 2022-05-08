
using Main.DAL.Concrete;
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class CrimeStats : BasicCrimeStats
    {
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

        public CrimeStats(JToken? data) : base(data)
        {
            Arson = (int?)data?["arson"] ?? 0;
            Assault = (int?)data?["aggravated_assault"] ?? 0;
            Burglary = (int?)data?["burglary"] ?? 0;
            Homicide = (int?)data?["homicide"] ?? 0;
            Larceny = (int?)data?["larceny"] ?? 0;
            MotorVehicleTheft = (int?)data?["motor_vehicle_theft"] ?? 0;
            RapeLegacy = (int?)data?["rape_legacy"] ?? 0;
            RapeRevised = (int?)data?["rape_revised"] ?? 0;
            Robbery = (int?)data?["robbery"] ?? 0;

        }

        public CrimeStats(CrimeStats stats) : base(stats)
        {
            Arson = stats.Arson;
            Assault = stats.Assault;
            Burglary = stats.Burglary;
            Homicide = stats.Homicide;
            HumanTrafficking = stats.HumanTrafficking;
            Larceny = stats.Larceny;
            MotorVehicleTheft = stats.MotorVehicleTheft;
            RapeLegacy = stats.RapeLegacy;
            RapeRevised = stats.RapeRevised;
            Robbery = stats.Robbery;

        }

    }

}
