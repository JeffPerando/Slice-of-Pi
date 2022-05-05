
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface IAPICacheService<T> where T : APICache
    {
        Task<string?> FetchStrAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);
        string? FetchStr(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true) =>
            FetchStrAsync(endpoint, query, cacheQuery).GetAwaiter().GetResult();

        Task<JObject?> FetchJObjectAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);
        JObject? FetchJObject(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true) =>
            FetchJObjectAsync(endpoint, query, cacheQuery).GetAwaiter().GetResult();

    }

}
