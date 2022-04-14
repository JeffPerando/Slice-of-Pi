
using Main.DAL.Abstract;
using Main.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        private readonly string _apiKey;
        private readonly IWebService _client;

        public ATTOMService(IConfiguration config) : this(config["ATTOMKey"]) {}

        public ATTOMService(string apiKey) : this(apiKey, new WebService()) {}

        public ATTOMService(string apiKey, IWebService web)
        {
            _apiKey = apiKey;
            _client = web;
            _client.AddHeader("apikey", _apiKey);

        }

        private T? FetchATTOM<T>(string endpoint, Dictionary<string, string?>? query = null)
        {
            string url = "https://api.gateway.attomdata.com/propertyapi/v1.0.0";
            
            if (!endpoint.StartsWith('/'))
            {
                url += '/';
            }

            return _client.FetchInto<T>(url + endpoint, query);
        }

        /*
        public async Task<List<HouseAssessment>> GetPriceHistory(Home addr)
        {
            var prices = new List<HouseAssessment>();

            var priceData = await FetchATTOMAsync("", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2
            });

            return prices;
        }
        */

        //TODO implement
        public int GetAssessmentFor(Home address)
        {
            throw new NotImplementedException();
        }

    }

}
