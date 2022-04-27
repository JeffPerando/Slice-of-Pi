
using Main.Models;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIv2
    {
        public Crime? StateCrimeSingle(string state, int? year = null);

        public List<Crime> StateCrimeRange(string state, int startYear, int endYear);

        public Crime? CityCrimeSingle(string city, string state, int? year = null);

        public List<Crime> CityCrimeRange(string city, string state, int startYear, int endYear);

    }

}
