using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class Home
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; } = null!;
        public string StreetAddress2 { get { return $"{County}, {State} {ZipCode}"; } }
        public string ZipCode { get; set; } = null!;
        public string State { get; set; } = null!;
        public string County { get; set; } = null!;
        public double Price { get; set; }
        public string UserId { get; set; } = null!;
        public int? AgencyId { get; set; }

        public virtual AgencyInformation? Agency { get; set; }
        public virtual User User { get; set; } = null!;

    }

}
