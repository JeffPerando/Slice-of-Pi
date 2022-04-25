
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Main.Helpers
{
    public class FileHelper
    {
        private static readonly string root = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"wwwroot\"));

        public static string ReadStr(string path)
        {
            return System.IO.File.ReadAllText(root + path);
        }

        public static JObject ReadJObj(string path)
        {
            return JObject.Parse(ReadStr(path));
        }

        public static JArray ReadJArray(string path)
        {
            return JArray.Parse(ReadStr(path));
        }

        public static T? ReadInto<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(ReadStr(path));
        }

    }

}
