
using Main.DAL.Abstract;
using Main.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Main.DAL.Concrete
{
    public class APICacheService : IAPICacheService
    {
        private readonly CrimeDbContext _db;
        private readonly DbSet<APICache> _set;
        private readonly TimeSpan _expiryOffset;

        public APICacheService(CrimeDbContext db, DbSet<APICache> set, TimeSpan? expiryOffset = null)
        {
            _db = db;
            _set = set;
            //Default expiry time is 30 days
            _expiryOffset = expiryOffset ?? new TimeSpan(30, 0, 0, 0, 0);

        }

        public string? Fetch(string endpoint, Dictionary<string, string?>? query = null)
        {
            string url = endpoint;

            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            var result = _set.Find(url);

            if (result != null)
            {
                if (result.Expiry > DateTime.Now)
                {
                    return null;
                }

            }

            return result?.Data;
        }

        public bool Cache(string data, string endpoint, Dictionary<string, string?>? query = null)
        {
            string url = endpoint;

            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            var cached = new APICache
            {
                URL = url,
                Expiry = DateTime.Now + _expiryOffset,
                Data = data
            };

            _set.Add(cached);
            return _db.SaveChanges() > 0;
        }

    }

}
