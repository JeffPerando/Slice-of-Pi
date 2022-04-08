using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = null!;
        [Display(Name = "Home Address")]
        public string Address { get; set; } = null!;

        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<StateCrimeSearchResult> StateCrimeSearchResults { get; set; }
    }
}
