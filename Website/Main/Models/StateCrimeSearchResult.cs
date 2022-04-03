using System;
using System.Collections.Generic;

namespace Main.Models
{
    public partial class StateCrimeSearchResult
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime DateSearched { get; set; }
        public string State { get; set; } = null!;
        public int Year { get; set; }
        public int Population { get; set; }
        public int ViolentCrimes { get; set; }
        public int Homicide { get; set; }
        public int RapeLegacy { get; set; }
        public int RapeRevised { get; set; }
        public int Robbery { get; set; }
        public int Assault { get; set; }
        public int PropertyCrimes { get; set; }
        public int Burglary { get; set; }
        public int Larceny { get; set; }
        public int MotorVehicleTheft { get; set; }
        public int Arson { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
