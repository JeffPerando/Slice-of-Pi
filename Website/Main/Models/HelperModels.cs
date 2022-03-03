using System;

namespace Main.Models
{
    public class StateCrimePair
    {
        public string StateAbbreviation { get; set; }
        public string CrimeAmount { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                StateCrimePair t = (StateCrimePair)obj;
                return (StateAbbreviation == t.StateAbbreviation) && (CrimeAmount == t.CrimeAmount);
            }
        }

        // always override this with equals so it can be stored in a dict
        public override int GetHashCode()
        {
            return new[] { StateAbbreviation, CrimeAmount }.GetHashCode();
        }
    }

    public class JSONYearVariable
    {
        public string JSONVariableTwoYears = '/' + (DateTime.Now.Year - 2).ToString() + '/' + (DateTime.Now.Year - 2).ToString();
    }

}