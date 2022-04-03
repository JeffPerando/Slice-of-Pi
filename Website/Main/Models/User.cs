using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class User
    {
        public User()
        {
            Homes = new HashSet<Home>();
            StateCrimeSearchResults = new HashSet<StateCrimeSearchResult>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<StateCrimeSearchResult> StateCrimeSearchResults { get; set; }
    }
}
