using Newtonsoft.Json;

namespace Main.Models
{
    public class State
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("abbreviation")]
        public string Abbrev { get; set; }

    }

}
