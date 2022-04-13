
using Newtonsoft.Json;

namespace Main.Models.ATTOM
{
    public class EchoedFields
    {
        [JsonProperty("jobID")]
        public string JobID { get; set; }

        [JsonProperty("loanNumber")]
        public string LoanNumber { get; set; }

        [JsonProperty("preparedBy")]
        public string PreparedBy { get; set; }

        [JsonProperty("resellerID")]
        public string ResellerID { get; set; }

        [JsonProperty("preparedFor")]
        public string PreparedFor { get; set; }
    }

    public class Exemption {}

    public class Exemptiontype {}

    public class ATTOMProperty
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("echoed_fields")]
        public EchoedFields EchoedFields { get; set; }

        [JsonProperty("property")]
        public List<Property> Property { get; set; }

    }

}
