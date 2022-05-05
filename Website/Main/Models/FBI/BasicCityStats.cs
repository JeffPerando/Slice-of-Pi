
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class BasicCityStats : BasicCrimeStats
    {
        public BasicCityStats(int year, JToken? data)
        {
            Year = year;

            if (data == null)
                return;

            var crimes = data.Where(data => (int?)data["year"] == year);

            foreach (var crime in crimes)
            {
                int amount = (int?)crime["actual"] ?? 0;
                var offense = crime["offense"]?.ToString();

                switch (offense)
                {
                    case "violent-crime": ViolentCrimes += amount; break;
                    case "property-crime": PropertyCrimes += amount; break;
                }

            }

        }

    }

}
