
namespace Main.Models
{
    public class APICache
    {
        public string Endpoint { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public string? Data { get; set; }

    }

    public class FBICache : APICache { }
    public class ATTOMCache : APICache { }

}
