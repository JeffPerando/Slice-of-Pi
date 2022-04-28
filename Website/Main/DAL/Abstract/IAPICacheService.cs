
namespace Main.DAL.Abstract
{
    public interface IAPICacheService
    {
        string? Fetch(string endpoint, Dictionary<string, string?>? query = null);

        bool Cache(string data, string endpoint, Dictionary<string, string?>? query = null);

    }

}
