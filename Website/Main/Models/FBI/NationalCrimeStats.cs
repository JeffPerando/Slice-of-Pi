
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class NationalCrimeStats : CrimeStats
    {
        public int Population { get; set; }
        public double CrimePerCapita
        {
            get
            {
                if (Population == 0)
                    return 0;
                return Math.Round(((double)TotalOffenses / Population) * 100_000, 2);
            }
        }

        public NationalCrimeStats() {}

        public NationalCrimeStats(JToken? data) : base(data)
        {
            Population = (int?)data?["populatuion"] ?? 0;

        }

    }

}
