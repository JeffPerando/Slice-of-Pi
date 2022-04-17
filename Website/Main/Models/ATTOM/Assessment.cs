
using Newtonsoft.Json;

namespace Main.Models.ATTOM
{
    public class ATTOMAssessment
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("property")]
        public List<Property> Property { get; set; }

    }

}
