
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class CityCrimeStats : CrimeStats
    {
        public string City { get; set; } = "";
        public State State { get; set; } = new();
        //public int ActualConvictions { get; set; }

        public CityCrimeStats() { }

        public CityCrimeStats(string city, State state, int year, JToken? data)
        {
            City = city;
            State = state;

            if (data == null)
                return;

            var crimes = data.Where(a => (int?)a["year"] == year);

            foreach (JObject crime in crimes)
            {
                int amount = (int?)crime["actual"] ?? 0;
                var offense = crime["offense"]?.ToString();

                switch (offense)
                {
                    case null: continue;
                    case "aggravated-assault": Assault += amount; break;
                    case "arson": Arson += amount; break;
                    case "burglary": Burglary += amount; break;
                    case "homicide": Homicide += amount; break;
                    case "human-trafficing": HumanTrafficking += amount; break;
                    case "larceny": Larceny += amount; break;
                    case "motor-vehicle-theft": MotorVehicleTheft += amount; break;
                    case "property-crime": PropertyCrimes += amount; break;
                    case "rape": RapeRevised += amount; break;
                    case "rape-legacy": RapeLegacy += amount; break;
                    case "robbery": Robbery += amount; break;
                    case "violent-crime": ViolentCrimes += amount; break;
                }

            }

        }

        public CityCrimeStats(CityCrimeStats stats) : base(stats)
        {
            City = stats.City;
            State = stats.State;

        }

    }

}
