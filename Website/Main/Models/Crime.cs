using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class Crime
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string State { get; set; }
        public string OffenseType { get; set; } = null!;
        public int TotalOffenses { get; set; }
        public int ActualConvictions { get; set; }
        public int? AgencyId { get; set; }
        public string Population { get; set; }
        public float Crime_Per_Capita {get; set;}

        public virtual AgencyInformation? Agency { get; set; }
    }
}
