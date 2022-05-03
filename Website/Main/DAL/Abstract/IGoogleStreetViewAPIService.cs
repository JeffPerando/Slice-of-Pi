using Main.Models.Listings;

namespace Main.DAL.Abstract
{
    public interface IGoogleStreetViewAPIService
    {
        public string ParseAddress(string address);
        public string GetStreetView(string address);

    }
}
