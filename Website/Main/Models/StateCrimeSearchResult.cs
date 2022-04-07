using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public StateCrimeSearchResult PresentJSONRespone(JObject info)
        {
            foreach (var item in info["results"])
            {
                try
                {
                    State = (string?)item["state_abbr"] ?? "N/A";
                    Year = (int?)item["year"] ?? 0;
                    Population = (int?)item["population"] ?? 0;

                    ViolentCrimes = (int?)item["violent_crime"] ?? 0;
                    Homicide = (int?)item["homicide"] ?? 0;
                    RapeLegacy = (int?)item["rape_legacy"] ?? 0;
                    RapeRevised = (int?)item["rape_revised"] ?? 0;
                    Robbery = (int?)item["robbery"] ?? 0;
                    Assault = (int?)item["aggravated_assault"] ?? 0;
                    PropertyCrimes = (int?)item["property_crime"] ?? 0;
                    Burglary = (int?)item["burglary"] ?? 0;
                    Larceny = (int?)item["larceny"] ?? 0;
                    MotorVehicleTheft = (int?)item["motor_vehicle_theft"] ?? 0;
                    Arson = (int?)item["arson"] ?? 0;

                }
                catch
                {
                    continue;
                }
            }

            return this;
        }

    }
}
