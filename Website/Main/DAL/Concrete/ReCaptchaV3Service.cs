
using Main.DAL.Abstract;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class ReCaptchaV3Result
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("score")]
        public float Score { get; set; }

    }

    public class ReCaptchaV3Service : IReCaptchaService
    {
        private readonly IWebService _web;
        private readonly string _key;
        private readonly float _threshold;

        public ReCaptchaV3Service(IConfiguration config) : this(new WebService(), config["captchaServerKey"]) {}

        public ReCaptchaV3Service(IWebService web, string serverKey, float threshold = 0.5f)
        {
            _web = web;
            _key = serverKey;
            _threshold = threshold;

        }

        public bool Passes(string response)
        {
            var url = "https://www.google.com/recaptcha/api/siteverify";
            
            var result = _web.FetchInto<ReCaptchaV3Result>(url, new()
            {
                ["secret"] = _key,
                ["response"] = response
            });

            if (result == null)
            {
                return false;
            }

            return result.Success && result.Score >= _threshold;
        }

    }

}
