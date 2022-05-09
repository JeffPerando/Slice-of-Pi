using Main.DAL.Abstract;
using Main.Models;
using Main.Models.ATTOM;
using Main.Models.Listings;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Main.DAL.Concrete
{
    public class GoogleStreetViewAPIService : IGoogleStreetViewAPIService
    {
        public readonly string GoogleStreetViewURL = "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=";

        private readonly string _apiKey;

        private readonly string _privateAuthKey;

        private readonly IWebService _web;
        public GoogleStreetViewAPIService(IConfiguration config, IWebService web) : this(config["GoogleKey"], web, config["PrivateGoogleAuthKey"]) { }


        public GoogleStreetViewAPIService(string googleKey, IWebService web, string authKey)
        {
            _apiKey = googleKey;
            _web = web;
            _privateAuthKey = authKey;
        }

        public static string Sign(string url, string keyString)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    
    public string ParseAddress(string address)
        {
            char[] charArray = address.ToCharArray();

            for(int i = 0; i < charArray.Length; i++)
            {
                if(charArray[i] == ' ')
                {
                    charArray[i]= '+';
                }
            }

            address = new string(charArray);

            return address;
        }

        public string GetStreetView(string address)
        {
            StreetViewViewModel viewModel = new StreetViewViewModel();

            address = ParseAddress(address);

            string apiCall = GoogleStreetViewURL + address + "&key=" + _apiKey;

            apiCall = Sign(apiCall, _privateAuthKey);

            return apiCall;
        }
    }
}
