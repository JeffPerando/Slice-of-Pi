
using Main.Models;

namespace Main.DAL.Abstract
{
    public interface IHousingAPI
    {
        //public Task<List<HouseAssessment>> GetPriceHistory(Home address);
        public HomeAssessment? GetAssessmentFor(Home address);

    }

}
