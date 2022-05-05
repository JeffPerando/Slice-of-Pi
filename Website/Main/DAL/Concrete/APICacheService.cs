
using Main.DAL.Abstract;
using Main.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class APICacheService<T> : IAPICacheService<T> where T : APICache, new()
    {
        private string _baseURL = "";

        private readonly IWebService _web;
        private readonly CrimeDbContext _db;
        private readonly DbSet<T> _set;
        private readonly TimeSpan _expiryOffset;

        public APICacheService(IWebService web, CrimeDbContext db, TimeSpan? expiryOffset = null)
        {
            _web = web;
            _db = db;
            _set = db.Set<T>();
            //Default expiry time is 30 days
            _expiryOffset = expiryOffset ?? new TimeSpan(30, 0, 0, 0, 0);

        }

        public IWebService Web() => _web;

        public IAPICacheService<T> SetBaseURL(string url)
        {
            _baseURL = url;

            if (url.EndsWith('/'))
            {
                _baseURL = url[0..^1];
            }

            return this;
        }

        public IAPICacheService<T> AddHeader(string key, string? value)
        {
            _web.AddHeader(key, value);
            return this;
        }

        public string? FetchStr(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true)
        {
            //all endpoints should start with /. this is to increase readability inside the actual database
            if (!endpoint.StartsWith('/'))
            {
                endpoint = $"/{endpoint}";
            }

            var endpointQ = endpoint;

            //we add the query string manually since it can influence what we get out of the fetch
            if (query != null)
            {
                endpointQ = QueryHelpers.AddQueryString(endpoint, query).Replace(",", "%2C");
            }

            var result = _set.Find(cacheQuery ? endpointQ : endpoint);

            //if the cached entry isn't expired, use it
            if (result != null && result.Expiry > DateTime.Now)
            {
                return result.Data;
            }

            var data = _web.FetchStr(_baseURL + endpointQ);

            var entry = new T
            {
                Endpoint = cacheQuery ? endpointQ : endpoint,
                Expiry = DateTime.Now + _expiryOffset,
                Data = data
            };

            _set.Add(entry);
            _db.SaveChanges();

            return data;
        }

        public JObject? FetchJObject(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true)
        {
            var result = FetchStr(endpoint, query, cacheQuery);

            if (result == null)
                return null;

            return JObject.Parse(result);
        }

    }

}
