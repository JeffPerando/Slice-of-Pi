
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.Listings;

namespace Main.DAL.Concrete
{
    public class MockATTOMService : IHousingAPI
    {
        public MockATTOMService() {}

        public virtual HomeAssessment? GetAssessmentFor(Home address)
        {
            throw new NotImplementedException();
        }

        public virtual AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy)
        {
            throw new NotImplementedException();
        }

    }

}
