
using Main.DAL.Abstract;
using Main.Models;
using Main.Models.ATTOM;
using Main.Models.Listings;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        public readonly string ATTOMUrl = "https://api.gateway.attomdata.com/propertyapi/v1.0.0/";

        private readonly string _apiKey;
        private readonly IWebService _web;

        public ATTOMService(IConfiguration config, IWebService web) : this(config["ATTOMKey"], web) { }

        public ATTOMService(string apiKey, IWebService web)
        {
            _apiKey = apiKey;
            _web = web;
            _web.AddHeader("apikey", _apiKey);

        }

        private JObject? FetchATTOMObj(string endpoint, Dictionary<string, string?>? query = null)
        {
            if (endpoint.StartsWith('/'))
            {
                endpoint = endpoint[1..];
            }

            return _web.FetchJObject(ATTOMUrl + endpoint, query);
        }

        private T? FetchATTOM<T>(string endpoint, Dictionary<string, string?>? query = null)
        {
            if (endpoint.StartsWith('/'))
            {
                endpoint = endpoint[1..];
            }

            return _web.FetchInto<T>(ATTOMUrl + endpoint, query);
        }

        public HomeAssessment? GetAssessmentFor(Home addr)
        {
            var result = FetchATTOM<ATTOMAssessment>("/assessment/detail", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2,
            });

            var assess = result?.Property?.FirstOrDefault()?.Assessment;

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

        public string SetNullResponse()
        {
            string zipcode  = "97304";
            string minPrice = "100000";
            string maxPrice = "600000";
            string pages    = "50";
            string endpoint = "assessment/detail?postalcode=" + zipcode + "&minAssdTtlValue=" + minPrice + "&maxAssdTtlValue=" + maxPrice + "&pagesize=" + pages;

            var x = ATTOMUrl + endpoint;
            var info = _web.FetchJObject(x);
            string? response = info.ToString();

            return response;
        }

        private AttomJson? FetchNullResponse()
        {
            return FetchATTOM<AttomJson>("assessment/detail", new()
            {
                ["postalcode"] = "97304",
                ["minAssdTtlValue"] = "100000",
                ["maxAssdTtlValue"] = "600000",
                ["pagesize"] = "50"
            });
        }

        public AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy)
        {
            string endpoint = "assessment/detail?postalcode=" + zipcode + "&minAssdTtlValue=" + minPrice + "&maxAssdTtlValue=" + maxPrice + "&pagesize=" + pages;

            //if (orderBy != null)
            //{
            //    endpoint = OrderBy(orderBy, endpoint);
            //}

            var x = ATTOMUrl + endpoint;

            var info = _web.FetchJObject(x);

            if (info == null)
            {
                string? nullResponse = SetNullResponse();
                AttomJson nullResponseResult = new JavaScriptSerializer().Deserialize<AttomJson>(nullResponse);
                return nullResponseResult;
            }

            string response = info.ToString();


            AttomJson data = new JavaScriptSerializer().Deserialize<AttomJson>(response);

            return data;
        }

    }

}
