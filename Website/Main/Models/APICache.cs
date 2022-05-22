
using MongoDB.Bson.Serialization.Attributes;

namespace Main.Models
{
    public class APICache
    {
        public string Endpoint { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public string? Data { get; set; }

    }

    [BsonIgnoreExtraElements]
    public class FBICache : APICache {}

    [BsonIgnoreExtraElements]
    public class ATTOMCache : APICache {}

}
