using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class APIController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CrimeDbContext _db;

        public APIController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CrimeDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        [HttpGet]
        public IActionResult StateCrimeSearchResults(/*int page = 1*/)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Content("[]", "application/json");
            }

            //Commented out paging code since I don't want to implement it atm
            //var itemsPerPage = 10;

            var userID = _userManager.GetUserId(User);
            //var pageIndex = page - 1;

            var allResults = _db.StateCrimeSearchResults.Where(sr => sr.UserId == userID).OrderByDescending(scsr => scsr.DateSearched);
            var totalResultCount = allResults.Count();
            //var results = allResults.Skip(pageIndex * itemsPerPage).Take(itemsPerPage);

            return Json(new
            {
                //page = page,
                //totalPages = totalResultCount / itemsPerPage,
                results = allResults.Take(10)
            });
        }

    }

}
