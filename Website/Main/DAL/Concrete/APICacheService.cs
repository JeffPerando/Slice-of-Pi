
using Main.DAL.Abstract;
using Main.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class APICacheService<T> : IAPICacheService<T> where T : APICache, new()
    {
        private string _baseURL = "";

        private readonly IWebService _web;
        private readonly IMongoCollection<T> _db;
        private readonly TimeSpan _expiryOffset;

        public APICacheService(IWebService web, IMongoDatabase db, TimeSpan? expiryOffset = null)
        {
            _web = web;
            _db = db.GetCollection<T>(typeof(T).Name);
            //Default expiry time is 24 hours
            _expiryOffset = expiryOffset ?? new TimeSpan(0, 24, 0, 0, 0);

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

        public async Task<string?> FetchStrAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true)
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

            var dbEndpoint = (cacheQuery ? endpointQ : endpoint);

            var result = (await _db.FindAsync(e => e.Endpoint == dbEndpoint)).FirstOrDefault();

            //if the cached entry isn't expired, use it
            if (result != null && result.Expiry > DateTime.Now)
            {
                return result.Data;
            }
            
            var data = await _web.FetchStrAsync(_baseURL + endpointQ);

            if (data != null)
            {
                var entry = new T
                {
                    Endpoint = dbEndpoint,
                    Expiry = DateTime.Now + _expiryOffset,
                    Data = data
                };

                _db.InsertOneAsync(entry);

            }

            return data;
        }

        public async Task<JObject?> FetchJObjectAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true)
        {
            var result = await FetchStrAsync(endpoint, query, cacheQuery);

            if (result == null)
                return null;

            return JObject.Parse(result);
        }

        //this code took way too long and too much brainpower to work out
        //hence the actual documentation
        //i'm sorry for the LINQ abuse
        public async Task<List<string?>> MultifetchStrsAsync(IEnumerable<string> endpoints, Dictionary<string, string?>? query = null, bool cacheQuery = true)
        {
            //so we take the endpoints and ensure they start with / for better concatenation
            var endpointList = endpoints.Select(e => e.StartsWith('/') ? e : $"/{e}").ToList();

            //Lists are annoying, but a necessary evil. So I make a list of tri-tuples. This makes sense later.
            //Fun fact: The Original endpoint is useless, save for maybe debugging.
            var allEndpoints = endpointList.Select(e =>
            {
                var eQuery = query == null ? e : QueryHelpers.AddQueryString(e, query).Replace(",", "%2C");
                return (Original: e, Query: eQuery, DB: (cacheQuery ? eQuery : e));
            });

            //Filter and fetch the cached results using the database endpoints and expiry
            var matchEndpoint = Builders<T>.Filter.In<string>(e => e.Endpoint, allEndpoints.Select(e => e.DB));
            var expiry = Builders<T>.Filter.Where(e => e.Expiry > DateTime.UtcNow);//we want the expiration date in the FUTURE, therefore greater than, the current date

            //Yes, this is valid. See:
            //https://stackoverflow.com/questions/32227284/mongo-c-sharp-driver-building-filter-dynamically-with-nesting
            var results = (await _db.FindAsync(expiry & matchEndpoint)).ToList();

            //If every single endpoint is cached, we just return all the data.
            if (results.Count == endpointList.Count)
            {
                return results.Select(e => e.Data).ToList();
            }

            //This is the part where things suck

            //Note we use the tri-tuple list here. This keeps all the different strings together.
            var failures = allEndpoints;

            //We do an optimization: if we didn't find anything, then all endpoints are failures.
            if (results.Count > 0)
            {
                //We get the list of failures, which is the inverse of the list of successes.
                var successes = results.Select(r => r.Endpoint);
                //Note that we compare with the DB endpoint
                failures = allEndpoints.Where(e => !successes.Contains(e.DB)).ToList();

            }

            //Now we fetch using the query endpoints, and associate the tri-tuple with the fetch via yet another tuple
            var fetches = failures.Select(e => (Endpoint: e, Fetch: _web.FetchStrAsync(_baseURL + e.Query)) ).ToList();

            //Wait for the fetches to come back
            Task.WaitAll(fetches.Select(t => t.Fetch).ToArray());

            //Now we get the fetches that were successfull and cache them
            var fetchResults = fetches.Select(t =>
                //Note we use the DB endpoint here; we have no need for the other endpoints
                (Endpoint: t.Endpoint.DB, Result: t.Fetch.Result) );

            //If we found any fetches that returned valid data, cache them.
            var cacheResults = fetchResults.Where(r => r.Result != null);

            if (cacheResults.Any())
            {
                //No await since writing takes forever and the results won't be used by this method
                _db.InsertManyAsync(cacheResults.Select(r => new T
                {
                    Endpoint = r.Endpoint,
                    Expiry = DateTime.Now + _expiryOffset,
                    Data = r.Result
                }).ToList());

            }

            //Get the raw results and bring 'em home
            return fetchResults.Select(t => t.Result).ToList();
        }

        public async Task<List<JObject?>> MultifetchJObjectsAsync(IEnumerable<string> endpoints, Dictionary<string, string?>? query = null, bool cacheQuery = true)
        {
            return (await MultifetchStrsAsync(endpoints, query, cacheQuery)).Select(str => str == null ? null : JObject.Parse(str)).ToList();
        }

    }

}
