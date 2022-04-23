
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.Listings;

namespace Main.DAL.Concrete
{
    public class DummyHousingService : IHousingAPI
    {
        private readonly Dictionary<Tuple<string, string>, HomeAssessment?> _assessments;

        public DummyHousingService(Dictionary<Tuple<string, string>, HomeAssessment?> assessments)
        {
            _assessments = assessments;

        }

        public HomeAssessment? GetAssessmentFor(Home address)
        {
            return _assessments.GetValueOrDefault(new(address.StreetAddress, address.StreetAddress2), null);
        }

        public AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy)
        {
            throw new NotImplementedException();
        }

    }

}
