
using Main.Models.FBI;
using System.Text.Json.Serialization;

namespace Main.Models
{
    public partial class StateCrimeSearchResult
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; } = null!;
        public DateTime DateSearched { get; set; }
        public string State { get; set; } = null!;
        public int Year { get; set; }
        public int? Population { get; set; }
        public int? ViolentCrimes { get; set; }
        public int? Homicide { get; set; }
        public int? RapeLegacy { get; set; }
        public int? RapeRevised { get; set; }
        public int? Robbery { get; set; }
        public int? Assault { get; set; }
        public int? PropertyCrimes { get; set; }
        public int? Burglary { get; set; }
        public int? Larceny { get; set; }
        public int? MotorVehicleTheft { get; set; }
        public int? Arson { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; } = null!;

        public StateCrimeSearchResult(string UID, StateCrimeStats stats)
        {
            UserId = UID;
            DateSearched = DateTime.Now;
            Population = stats.Population;

            Year = stats.Year;
            ViolentCrimes = stats.ViolentCrimes;
            Homicide = stats.Homicide;
            RapeLegacy = stats.RapeLegacy;
            RapeRevised = stats.RapeRevised;
            Robbery = stats.Robbery;
            Assault = stats.Assault;
            PropertyCrimes = stats.PropertyCrimes;
            Burglary = stats.Burglary;
            Larceny = stats.Larceny;
            MotorVehicleTheft = stats.MotorVehicleTheft;
            Arson = stats.Arson;

        }

    }

}
