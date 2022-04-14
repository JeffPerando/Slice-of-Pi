
using Main.DAL.Abstract;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class WebResult
    {
        public bool Success { get; set; } = false;
        public string Content { get; set; } = "";

    }

    public class WebService : IWebService
    {
        /* 
         * There's some debate on how HttpClients should be distributed.
         * Some believe they should be static; others, one per controller.
         * There is an IHttpClientFactory or somesuch, which is a service.
         * BUT, our APIs are singletons. Therefore, we don't bother.
         * Note: Refactor services with better dependency injection and their own config handling
         */
        private readonly HttpClient _client = new();

        public WebService() {}
        
        /*
        public WebService(HttpClient client)
        {
            _client = client;
        }
        */

        public void AddHeader(string key, string? value)
        {
            _client.DefaultRequestHeaders.Add(key, value);

        }

        public HttpResponseMessage? FetchRaw(string url, Dictionary<string, string?>? query = null)
        {
            HttpResponseMessage? result = null;

            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            //Debug.WriteLine($"Fetching {url}");

            try
            {
                result = _client.GetAsync(url).GetAwaiter().GetResult();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return result;
        }

        public string? FetchStr(string url, Dictionary<string, string?>? query = null)
        {
            var result = FetchRaw(url);

            if (result == null || !result.IsSuccessStatusCode)
            {
                //Debug.WriteLine($"ERROR: {url} came back with {result?.StatusCode ?? 0}");
                return null;
            }

            return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public JObject? FetchJObject(string url, Dictionary<string, string?>? query)
        {
            var result = FetchStr(url);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JObject.Parse(result);
        }
        
        public JArray? FetchJArray(string url, Dictionary<string, string?>? query)
        {
            var result = FetchStr(url);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JArray.Parse(result);
        }

        public T? FetchInto<T>(string url, Dictionary<string, string?>? query = null)
        {
            return JsonConvert.DeserializeObject<T>(FetchStr(url, query) ?? "{}");
        }

        public async Task<HttpResponseMessage> FetchRawAsync(string url, Dictionary<string, string?>? query = null)
        {
            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            return await _client.GetAsync(url);
        }

        public async Task<string?> FetchStrAsync(string url, Dictionary<string, string?>? query = null)
        {
            //I hate callback hell
            var result = await FetchRawAsync(url, query);

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<JObject?> FetchJObjectAsync(string url, Dictionary<string, string?>? query = null)
        {
            var result = await FetchStrAsync(url, query);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JObject.Parse(result);
        }

        public async Task<JArray?> FetchJArrayAsync(string url, Dictionary<string, string?>? query = null)
        {
            var result = await FetchStrAsync(url, query);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JArray.Parse(result);
        }

        public async Task<T?> FetchIntoAsync<T>(string url, Dictionary<string, string?>? query = null)
        {
            var result = await FetchStrAsync(url, query);
            return JsonConvert.DeserializeObject<T>(result ?? "{}");
        }

    }

}
