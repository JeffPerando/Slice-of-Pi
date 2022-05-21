
using Main.DAL.Abstract;
using Main.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace Main.DAL.Concrete
{
    public class SiteUserService : ISiteUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CrimeDbContext _db;

        public SiteUserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CrimeDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        public bool IsLoggedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public bool HasMFAEnabled(ClaimsPrincipal user)
        {
            var idUser = _userManager.GetUserAsync(user).GetAwaiter().GetResult();

            return idUser.TwoFactorEnabled;
        }

        public string ID(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public User? Data(ClaimsPrincipal user)
        {
            var id = ID(user);

            if (id == null)
            {
                return null;
            }

            var data = _db.Users.Find(id);

            if (data == null)
            {
                var identity = _userManager.GetUserAsync(user).GetAwaiter().GetResult();

                data = new User
                {
                    Id = id,
                    Name = "J. Doe",
                    EmailAddress = identity.Email
                };

                _db.Users.Add(data);
                _db.SaveChanges();

            }

            return data;
        }

        public string Name(ClaimsPrincipal user)
        {
            if (!IsLoggedIn(user))
            {
                return "INVALID_USER";
            }

            return Data(user)?.Name ?? "John Doe";
        }

        public IEnumerable<Home> Addresses(ClaimsPrincipal user)
        {
            var id = ID(user);

            if (id == null)
            {
                //IntelliSense scares me sometimes
                return Enumerable.Empty<Home>();
            }

            return _db.Homes.Where(addr => addr.UserId == id);
        }

        public bool AddAddress(ClaimsPrincipal user, Home addr)
        {
            var id = ID(user);

            if (id == null)
            {
                return false;
            }
            
            addr.UserId = id;
            
            _db.Homes.Add(addr);
            _db.SaveChanges();

            return true;
        }

        public List<StateCrimeSearchResult>? StateCrimeSearchResults(ClaimsPrincipal user, int? limit = null)
        {
            var id = ID(user);

            IQueryable<StateCrimeSearchResult> results = _db.StateCrimeSearchResults.Where(scsr => scsr.UserId == id).OrderByDescending(scsr => scsr.DateSearched);

            //Commented out paging code since I don't want to implement it atm
            //var itemsPerPage = 10;
            //var pageIndex = page - 1;
            //var results = allResults.Skip(pageIndex * itemsPerPage).Take(itemsPerPage);

            if (limit != null)
            {
                //good grief, this compiler eats glue
                //remove the null coalescing op. I dare you.
                results = results.Take(limit ?? int.MaxValue);
            }

            return results.ToList();
        }

    }

}
