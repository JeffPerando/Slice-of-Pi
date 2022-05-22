using Main.Models.Listings;

namespace Main.DAL.Abstract
{
    public interface IGoogleStreetViewAPIService
    {
        public string ParseAddress(string address);
        public string GetStreetView(string address);
        public string GetEmbededMap(string Address);
        public string ParseAddressEmbededMap(string address);
        public StreetViewViewModel ParseAddressSubmission(string address);
        public string ToUpperCase(string cityName);
    }
}
