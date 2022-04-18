
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.ATTOM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        private readonly string _apiKey;
        private readonly IWebService _web;

        public ATTOMService(IConfiguration config) : this(config["ATTOMKey"]) {}

        public ATTOMService(string apiKey) : this(apiKey, new WebService()) {}

        public ATTOMService(string apiKey, IWebService web)
        {
            _apiKey = apiKey;
            _web = web;
            _web.AddHeader("apikey", _apiKey);

        }

        private T? FetchATTOM<T>(string endpoint, Dictionary<string, string?>? query = null)
        {
            string url = "https://api.gateway.attomdata.com/propertyapi/v1.0.0";
            
            if (!endpoint.StartsWith('/'))
            {
                url += '/';
            }

            return _web.FetchInto<T>(url + endpoint, query);
        }

        public int? GetAssessmentFor(Home addr)
        {
            var result = FetchATTOM<ATTOMAssessment>("/assessment/detail", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2,
            });
            
            if (result?.Property == null)
            {
                return null;
            }

            return result.Property.First().Assessment?.Assessed.AssdTtlValue;
        }
        
    }

}
