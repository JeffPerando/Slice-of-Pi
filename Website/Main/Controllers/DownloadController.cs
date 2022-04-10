
using Main.Helpers;
using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Main.Controllers
{
    public class DownloadController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CrimeDbContext _db;

        public DownloadController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CrimeDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        public IActionResult StateCrimeSearchHistory()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            var userID = _userManager.GetUserId(User);

            var results = CSVParseHelper.fromStateSearchHistory(_db.StateCrimeSearchResults.Where(scsr => scsr.UserId == userID).OrderByDescending(scsr => scsr.DateSearched));
            return File(Encoding.UTF8.GetBytes(results), "application/octet", "StateCrimeSearchHistory.csv");
        }

    }

}
