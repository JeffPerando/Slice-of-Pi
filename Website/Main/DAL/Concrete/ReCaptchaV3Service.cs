
using Main.DAL.Abstract;
using System.Net;
//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.DAL.Concrete
{
    public class ReCaptchaV3Service : IReCaptchaService
    {
        private readonly string _key;
        private readonly float _threshold;

        public ReCaptchaV3Service(IConfiguration config) : this(config["captchaServerKey"]) {}

        public ReCaptchaV3Service(string serverKey, float threshold = 0.5f)
        {
            _key = serverKey;
            _threshold = threshold;

        }

        public async Task<bool> Passes(string response)
        {
            Debug.WriteLine($"Passing... {response}");
            using HttpClient client = new();
            var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_key}&response={response}";
            var req = await client.GetAsync(url);

            if (req.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            dynamic result = JObject.Parse(req.Content.ReadAsStringAsync().Result);
            bool pass = result.success == "true" && result.score >= _threshold;
            Debug.WriteLine($"Passed: {pass}");
            return pass;
        }

    }

}
