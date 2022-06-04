
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.ATTOM;
using Main.Models.Listings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        public readonly string ATTOMUrl = "https://api.gateway.attomdata.com/propertyapi/v1.0.0";

        private readonly string _apiKey;
        private readonly IAPICacheService<ATTOMCache> _cache;

        public ATTOMService(IConfiguration config, IAPICacheService<ATTOMCache> cache) : this(config["ATTOMKey"], cache) { }

        public ATTOMService(string apiKey, IAPICacheService<ATTOMCache> cache)
        {
            _apiKey = apiKey;
            _cache = cache
                .SetBaseURL(ATTOMUrl)
                .AddHeader("apikey", _apiKey);

        }

        public Home GetHouseInformation(string address1, string address2)
        {
            Home model = new Home();

            //model.StreetAddress = ("1665 185th Ave NE");
            //model.City = ("Bellevue");
            //model.County = ("King");
            //model.Price = (1143000);

            var result = _cache.FetchJObject("/property/basicprofile", new()
            {
                ["address1"] = address1,
                ["address2"] = address2,
            });

            if (result == null)
            {
                return model;
            }

            var mkt = (int?)(result["property"]?[0]?["assessment"]?["market"]?["mktTtlValue"]) ?? 0;
            var assess = (int)(result["property"][0]["assessment"]["assessed"]["assdTtlValue"]);

            model.StreetAddress = (string)(result["property"][0]["address"]["line1"]);
            model.City = (string)(result["property"][0]["address"]["locality"]);
            model.County = (string)(result["property"][0]["area"]["countrySecSubd"]);
            model.Price = Math.Max(mkt, assess);
            model.ZipCode = (string)(result["property"][0]["address"]["postal1"]);

            return model;
            
        }

        public HomeAssessment? GetAssessmentFor(Home addr)
        {
            var result = _cache.FetchInto<ATTOMAssessment>("/assessment/detail", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2,
            });

            var assess = result?.Property.FirstOrDefault()?.Assessment;

            if (assess == null)
            {
                return null;
            }

            return new HomeAssessment
            {
                MarketValue = assess.Market.MktTtlValue,
                AssessedValue = assess.Assessed.AssdTtlValue,
                TaxYear = assess.Tax.TaxYear ?? DateTime.Now.Year
            };

        }

        private string? FetchNullResponse()
        {
            return _cache.FetchStr("assessment/detail", new()
            {
                ["postalcode"] = "97304",
                ["minAssdTtlValue"] = "100000",
                ["maxAssdTtlValue"] = "600000",
                ["pagesize"] = "50"
            }) ?? "";
        }

        public AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy)
        {
            //if (orderBy != null)
            //{
            //    endpoint = OrderBy(orderBy, endpoint);
            //}

            var info = _cache.FetchStr("assessment/detail", new()
            {
                ["postalcode"] = zipcode,
                ["minAssdTtlValue"] = minPrice,
                ["maxAssdTtlValue"] = maxPrice,
                ["pagesize"] = pages
            }) ?? FetchNullResponse();

            var data = JsonConvert.DeserializeObject<AttomJson>(info);

            return data;
        }

    }

}
