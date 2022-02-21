using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class AgencyInformation
    {
        public AgencyInformation()
        {
            Crimes = new HashSet<Crime>();
            Homes = new HashSet<Home>();
        }

        public int Id { get; set; }
        public string Ori { get; set; } = null!;
        public string AgencyName { get; set; } = null!;
        public string AgencyCounty { get; set; } = null!;

        public virtual ICollection<Crime> Crimes { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
    }
}
