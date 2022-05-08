
using Main.Models;
using System.Security.Claims;

namespace Main.DAL.Abstract
{
    public interface ISiteUserService
    {
        bool IsLoggedIn(ClaimsPrincipal user);

        bool HasMFAEnabled(ClaimsPrincipal user);

        string ID(ClaimsPrincipal user);

        User? Data(ClaimsPrincipal user);

        string Name(ClaimsPrincipal user);

        IEnumerable<Home> Addresses(ClaimsPrincipal user);

        bool AddAddress(ClaimsPrincipal user, Home addr);

        public List<StateCrimeSearchResult>? StateCrimeSearchResults(ClaimsPrincipal user, int? limit = null);

    }

}
