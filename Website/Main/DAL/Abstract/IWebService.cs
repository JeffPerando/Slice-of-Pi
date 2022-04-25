﻿
using Newtonsoft.Json.Linq;

namespace Main.DAL.Abstract
{
    public interface IWebService
    {
        public void AddHeader(string key, string? value);

        public HttpResponseMessage? FetchRaw(string url, Dictionary<string, string?>? query = null);

        public string? FetchStr(string url, Dictionary<string, string?>? query = null);

        public JObject? FetchJObject(string url, Dictionary<string, string?>? query = null);

        public JArray? FetchJArray(string url, Dictionary<string, string?>? query = null);

        public T? FetchInto<T>(string url, Dictionary<string, string?>? query = null);

        //Async methods (please use if doing more than 1 request)

        public Task<HttpResponseMessage> FetchRawAsync(string url, Dictionary<string, string?>? query = null);

        public Task<string?> FetchStrAsync(string url, Dictionary<string, string?>? query = null);

        public Task<JObject?> FetchJObjectAsync(string url, Dictionary<string, string?>? query = null);

        public Task<JArray?> FetchJArrayAsync(string url, Dictionary<string, string?>? query = null);

        public Task<T?> FetchIntoAsync<T>(string url, Dictionary<string, string?>? query = null);


    }

}
