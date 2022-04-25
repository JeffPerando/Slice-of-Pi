
using Main.Models;
using Main.Models.Listings;
//using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface IHousingAPI
    {
        public HomeAssessment? GetAssessmentFor(Home address);
        public AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy);

    }
}
