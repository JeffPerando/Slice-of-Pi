
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;

namespace Main.DAL.Mock
{
    public class MockWebService : IWebService
    {
        public virtual void AddHeader(string key, string? value)
        {
            throw new NotImplementedException();
        }

        public virtual JArray? FetchJArray(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<JArray?> FetchJArrayAsync(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual JObject? FetchJObject(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<JObject?> FetchJObjectAsync(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual HttpResponseMessage? FetchRaw(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<HttpResponseMessage> FetchRawAsync(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual string? FetchStr(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<string?> FetchStrAsync(string url, Dictionary<string, string?>? query = null)
        {
            throw new NotImplementedException();
        }

    }

}
