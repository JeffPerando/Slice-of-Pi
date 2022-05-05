
using Main.DAL.Concrete;
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class BasicCrimeStats
    {
        public int Year { get; set; }
        public int ViolentCrimes { get; set; }
        public int PropertyCrimes { get; set; }
        public int TotalOffenses { get { return ViolentCrimes + PropertyCrimes; } }

        public BasicCrimeStats() {}

        public BasicCrimeStats(JToken? data)
        {
            Year = (int?)data?["year"] ?? FBIService.LatestYear;
            ViolentCrimes = (int?)data?["violent_crime"] ?? 0;
            PropertyCrimes = (int?)data?["property_crime"] ?? 0;

        }

        public BasicCrimeStats(int year, JToken? data)
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
                    case "violent-crime":   ViolentCrimes += amount; break;
                    case "property-crime":  PropertyCrimes += amount; break;
                }

            }

        }

    }

}
