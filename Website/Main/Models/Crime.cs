using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class Crime
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string State { get; set; } = null!;
        public string OffenseType { get; set; } = null!;
        public int TotalOffenses { get; set; }
        public int ActualConvictions { get; set; }
        public int? AgencyId { get; set; }
        public string Population { get; set; } = null!;
        public float CrimePerCapita { get; set; }

        public virtual AgencyInformation? Agency { get; set; }
    }
}
