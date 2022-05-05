
namespace Main.Models
{
    public partial class Crime
    {
        public int Year { get; set; }
        public string State { get; set; } = null!;
        public string OffenseType { get; set; } = null!;
        public int TotalOffenses { get; set; }
        public int ActualConvictions { get; set; }
        public int Population { get; set; }
        public double CrimePerCapita { get { if (Population == 0) return 0; return Math.Round(((double)TotalOffenses / Population) * 100000, 2); } }

    }

}
