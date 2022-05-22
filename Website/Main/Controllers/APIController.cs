
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Models;
using Main.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Main.Controllers
{
    public class APIController : Controller
    {
        private readonly IBackendService _backend;
        private readonly ISiteUserService _users;
        private readonly ICrimeAPIv2 _crime;
        private readonly ICrimeAPIService _crimeOld;
        private readonly CrimeDbContext _db;

        public APIController(IBackendService backend, ISiteUserService users, ICrimeAPIv2 crime, ICrimeAPIService crimeOld, CrimeDbContext db)
        {
            _backend = backend;
            _users = users;
            _crime = crime;
            _crimeOld = crimeOld;
            _db = db;

        }

        [HttpGet]
        public IActionResult States()
        {
            //return Json(_crime.GetStates());
            return Json(State.AllStates);
        }

        [HttpGet]
        public IActionResult GetCitiesIn(string? stateAbbrev)
        {
            return Json(_crime.CitiesIn(new State { Abbrev = stateAbbrev ?? "CA" }));
        }

        [HttpGet]
        public IActionResult GetSafestStates()
        {
            /*
            var state_list = _crime.GetStates();
            var get_national_stats = _crime.ReturnStateCrimeList(state_list);
            var top_five_states = _crime.GetSafestStates(get_national_stats);

            return Json(top_five_states);
            */
            return Json(_backend.CalcSafestStates().Select(s => new
            {
                State = s.State,
                Population = s.Population,
                CrimePerCapita = s.CrimePerCapita
            }));
        }

        [HttpGet]
        public IActionResult GetCityStats(string? cityName, string? stateAbbrev)
        {
            if (cityName == null || stateAbbrev == null)
            {
                cityName = "Riverside";
                stateAbbrev = "CA";
            }

            return Json(_crimeOld.GetCityStats(cityName, stateAbbrev));
            //return Json(_crime.CityCrimeSingle(cityName, new State { Abbrev = stateAbbrev }));
        }

        [HttpGet]
        public IActionResult UpdateCityStats(string? cityName, string? stateAbbrev, string? year)
        {
            return Json(_crimeOld.GetCityStatsByYear(cityName, stateAbbrev, year));
            // Json(_crime.CityCrimeSingle(cityName ?? "Riverside", new State { Abbrev = stateAbbrev ?? "CA" }, year));
        }

        [HttpGet]
        public IActionResult StateCrimeStats(int? year, string? stateAbbrev)
        {
            stateAbbrev ??= "CA";

            //var result = _crime.GetState(stateAbbrev, year);
            var data = _crime.StateCrimeSingle(new State { Abbrev = stateAbbrev }, year);

            if (_users.IsLoggedIn(User) && data != null)
            {
                var result = new StateCrimeSearchResult(_users.ID(User), data);
                
                _db.StateCrimeSearchResults.Add(result);
                _db.SaveChanges();

            }

            return Json(data);
        }

        [HttpGet]
        public IActionResult CityTrends(string? cityName, string? stateAbbrev)
        {
            if (cityName == null || stateAbbrev == null)
            {
                cityName = "Riverside";
                stateAbbrev = "CA";
            }
            /*
            var getCitytrends = _crimeOld.GetCityTrends(cityName, stateAbbrev);
            var returnTotalCityTrends = _crimeOld.ReturnTotalCityTrends(getCitytrends);
            var returnPropertyCityTrends = _crimeOld.ReturnPropertyCityTrends(getCitytrends);
            var returnViolentCityTrends = _crimeOld.ReturnViolentCityTrends(getCitytrends);


            return Json(new { totalTrends = returnTotalCityTrends, propertyTrends = returnPropertyCityTrends, violentTrends = returnViolentCityTrends });
            */
            return Json(_crime.CityCrimeRangeBasic(cityName, new State { Abbrev = stateAbbrev }, FBIService.OldestYear, FBIService.LatestYear).OrderBy(c => c.Year));
        }

        [HttpGet]
        public IActionResult StateCrimeSearchResults(/*int page = 1*/)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Content("[]", "application/json");
            }

            return Json(new
            {
                //page = page,
                //totalPages = totalResultCount / itemsPerPage,
                results = _users.StateCrimeSearchResults(User, 10)
            });
        }

        [HttpGet]
        public IActionResult NationalCrime(int? year)
        {
            return Json(new
            {
                year = year,
                stateCrimes = _crime.StateCrimeMulti(State.AllStates, year)
            });
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Content("test");
        }

    }

}
