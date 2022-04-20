
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.ATTOM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        private readonly string _apiKey;
        private readonly IWebService _web;

        public ATTOMService(IConfiguration config, IWebService web) : this(config["ATTOMKey"], web) {}

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

        public HomeAssessment? GetAssessmentFor(Home addr)
        {
            var result = FetchATTOM<ATTOMAssessment>("/assessment/detail", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2,
            });

            if (result?.Property == null)
            {
                Debug.WriteLine("Property not found");
                return null;
            }

            var assess = result.Property.First().Assessment;

            if (assess == null)
            {
                Debug.WriteLine("Assessment not found");
                return null;
            }

            return new HomeAssessment {
                MarketValue = assess.Market.MktTtlValue,
                AssessedValue = assess.Assessed.AssdTtlValue,
                TaxYear = assess.Tax.TaxYear ?? DateTime.Now.Year
            };
        }
        
    }

}
