
using Newtonsoft.Json.Linq;

namespace Main.Models.FBI
{
    public class StateCrimeStats : CrimeStats
    {
        public State State { get; set; } = new();
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

        public StateCrimeStats() {}

        public StateCrimeStats(State state, JToken? data) : base(data)
        {
            State = state;
            Population = (int?)data?["population"] ?? 0;

        }

        public StateCrimeStats(StateCrimeStats stats) : base(stats)
        {
            State = stats.State;
            Population = stats.Population;

        }

    }
    
}
