using Main.DAL.Abstract;
using Main.Models;
using Main.Models.Listings;
using Main.Models.ATTOM;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Diagnostics;
//using Main.Models;
//using Main.Models.ATTOM;
//using System.Net;
using Nancy.Json;

namespace Main.DAL.Concrete
{
    public class ATTOMService : IHousingAPI
    {
        private readonly string _apiKey = "";
        private readonly IWebService _web;
        public readonly string ATTOMUrl = "https://api.gateway.attomdata.com/propertyapi/v1.0.0/";

        private ATTOMKeyVerification _keyVerification = new ATTOMKeyVerification();

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

        public int GetAssessmentFor(Home addr)
        {
            var result = FetchATTOM<ATTOMAssessment>("/assessment/detail", new()
            {
                ["address1"] = addr.StreetAddress,
                ["address2"] = addr.StreetAddress2,
            });

            if (result?.Property == null)
            {
                return 0;
            }

            return result.Property.First().Assessment?.Assessed.AssdTtlValue ?? 0;
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

        public string OrderBy(string orderBy, string call)
        {
            if (orderBy != "None")
            {
                call = call + "&orderBy=AssdTtlValue+" + orderBy;
            }

            return call;
        }
        public AttomJson GetListing(string zipcode, string pages, string minPrice, string maxPrice, string? orderBy)
        {
            
            string endpoint = "assessment/detail?postalcode=" + zipcode + "&minAssdTtlValue=" + minPrice+ "&maxAssdTtlValue="+maxPrice+"&pagesize="+pages;

            if (orderBy != null)
            {
                endpoint = OrderBy(orderBy, endpoint);
            }

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