
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class StateCrimeStats
    {
        public State State { get; set; } = null!;
        public CrimeStats Stats { get; set; } = null!;
        public int Population { get; set; }
        public double CrimePerCapita
        {
            get
            {
                if (Population == 0)
                    return 0;
                return Math.Round(((double)Stats.TotalOffenses / Population) * 100_000, 2);
            }
        }

        public StateCrimeStats() {}

        public StateCrimeStats(State state, JObject data)
        {
            State = state;
            Stats = new CrimeStats(data);
            Population = (int?)data["population"] ?? 0;

        }

    }
    
}
