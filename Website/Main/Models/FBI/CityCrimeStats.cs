
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class CityCrimeStats
    {
        public string City { get; set; } = null!;
        public State State { get; set; } = null!;
        public CrimeStats? Stats { get; set; }
        public int TotalOffenses { get; set; }
        public int ActualConvictions { get; set; }

        public CityCrimeStats() {}

        public CityCrimeStats(string city, State state, int year, JToken data)
        {
            City = city;
            State = state;
            Stats = new CrimeStats();

            var crimes = data.Where(a => (int?)a["year"] == year);

            foreach (JObject crime in crimes)
            {
                int amount = (int?)crime["actual"] ?? 0;
                var offense = crime["offense"]?.ToString();

                switch (offense)
                {
                    case "aggravated-assault":  Stats.Assault += amount; break;
                    case "arson":               Stats.Arson += amount; break;
                    case "burglary":            Stats.Burglary += amount; break;
                    case "homicide":            Stats.Homicide += amount; break;
                    case "human-trafficing":    Stats.HumanTrafficking += amount; break;
                    case "larceny":             Stats.Larceny += amount; break;
                    case "motor-vehicle-theft": Stats.MotorVehicleTheft += amount; break;
                    case "property-crime":      Stats.PropertyCrimes += amount; break;
                    case "rape":                Stats.RapeRevised += amount; break;
                    case "rape-legacy":         Stats.RapeLegacy += amount; break;
                    case "robbery":             Stats.Robbery += amount; break;
                    case "violent-crime":       Stats.ViolentCrimes += amount; break;
                }

            }

        }

    }

}
