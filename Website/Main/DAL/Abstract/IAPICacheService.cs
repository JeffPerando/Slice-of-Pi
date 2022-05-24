
using Main.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface IAPICacheService<T> where T : APICache
    {
        IWebService Web();

        IAPICacheService<T> SetBaseURL(string url);

        IAPICacheService<T> AddHeader(string key, string? value);

        Task<string?> FetchStrAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);
        string? FetchStr(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true) =>
            FetchStrAsync(endpoint, query, cacheQuery).GetAwaiter().GetResult();

        Task<JObject?> FetchJObjectAsync(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);
        JObject? FetchJObject(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true) =>
            FetchJObjectAsync(endpoint, query, cacheQuery).GetAwaiter().GetResult();

        Task<List<string?>> MultifetchStrsAsync(IEnumerable<string> endpoints, Dictionary<string, string?>? query = null, bool cacheQuery = true);

        Task<List<JObject?>> MultifetchJObjectsAsync(IEnumerable<string> endpoints, Dictionary<string, string?>? query = null, bool cacheQuery = true);

        R? FetchInto<R>(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true) =>
            JsonConvert.DeserializeObject<R>(FetchStr(endpoint, query, cacheQuery) ?? "{}");

    }

}
