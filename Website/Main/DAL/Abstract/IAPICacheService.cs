
using Main.Models;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface IAPICacheService<T> where T : APICache
    {
        IAPICacheService<T> SetBaseURL(string url);

        string? FetchStr(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);

        JObject? FetchJObject(string endpoint, Dictionary<string, string?>? query = null, bool cacheQuery = true);

    }

}
