using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class APIController : Controller
    {
        private readonly ISiteUserService _users;
        private readonly ICrimeAPIService _crime;
        private readonly CrimeDbContext _db;
        private readonly ICrimeAPIService _crime;

        public APIController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CrimeDbContext db)
        {
            _users = users;
            _crime = crime;
            _db = db;
            _crime = crime;

        }

        [HttpGet]
        public IActionResult States()
        {
            var state_list = _crime.GetStates();
            return Json(state_list);
        }

        [HttpGet]
        public IActionResult SafestStates()
        {
            var state_list = _crime.GetStates();
            var get_national_stats = _crime.ReturnStateCrimeList(state_list);
            var top_five_states = _crime.GetSafestStates(get_national_stats);

            return Json(top_five_states);
        }

        [HttpGet]
        public IActionResult GetCityStats(string cityName, string stateAbbrev)
        {
            if (cityName == null || stateAbbrev == null)
            {
                cityName = "Riverside";
                stateAbbrev = "CA";
            }

            return Json(_crime.GetCityStats(cityName, stateAbbrev));
        }

        [HttpGet]
        public IActionResult UpdateCityStats(string cityName, string stateAbbrev, string year)
        {
            return Json(_crime.GetCityStatsByYear(cityName, stateAbbrev, year));
        }

        [HttpGet]
        public IActionResult StateCrimeStats(int? year, string? stateAbbrev)
        {
            year ??= 2020;
            stateAbbrev ??= "CA";

            var result = _crime.GetState(stateAbbrev, year);

            if (_users.IsLoggedIn(User))
            {
                result.UserId = _users.ID(User);
                result.DateSearched = DateTime.Now;

                _db.StateCrimeSearchResults.Add(result);
                _db.SaveChangesAsync();

            }

            return Json(result);
        }

        [HttpGet]
        public IActionResult CityTrends(string cityName, string stateAbbrev)
        {
            if (cityName == null || stateAbbrev == null)
            {
                cityName = "Riverside";
                stateAbbrev = "CA";
            }

            var getCitytrends = _crime.GetCityTrends(cityName, stateAbbrev);
            var returnTotalCityTrends = _crime.ReturnTotalCityTrends(getCitytrends);
            var returnPropertyCityTrends = _crime.ReturnPropertyCityTrends(getCitytrends);
            var returnViolentCityTrends = _crime.ReturnViolentCityTrends(getCitytrends);


            return Json(new { totalTrends = returnTotalCityTrends, propertyTrends = returnPropertyCityTrends, violentTrends = returnViolentCityTrends });
        }

        [HttpGet]
        public IActionResult StateCrimeSearchResults(/*int page = 1*/)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Content("[]", "application/json");
            }

            //Commented out paging code since I don't want to implement it atm
            //var itemsPerPage = 10;

            var userID = _users.ID(User);
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

        [HttpGet]
        public IActionResult NationalCrime(int year)
        {
            return Json(new
            {
                year = year,
                stateCrimes = _crime.GetStates()
                    //.Take(5)
                    .Select(state => _crime.GetState(state, year)).Where(scsr => scsr != null).ToList()
            });
        }

    }

}
