
using Main.Models;
using System.Security.Claims;

namespace Main.DAL.Abstract
{
    public interface ISiteUserService
    {
        public bool IsLoggedIn(ClaimsPrincipal user);

        public string ID(ClaimsPrincipal user);

        public User? Data(ClaimsPrincipal user);

        public string Name(ClaimsPrincipal user);

        public IEnumerable<Home> Addresses(ClaimsPrincipal user);

        public bool AddAddress(ClaimsPrincipal user, Home addr);

    }

}
