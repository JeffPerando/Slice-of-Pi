
using Main.DAL.Concrete;
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class BasicCrimeStats
    {
        public int Year { get; set; } = FBIService.LatestYear;
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

        public BasicCrimeStats(BasicCrimeStats stats)
        {
            Year = stats.Year;
            ViolentCrimes = stats.ViolentCrimes;
            PropertyCrimes = stats.PropertyCrimes;

        }

    }

}
