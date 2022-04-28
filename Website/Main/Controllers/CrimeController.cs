
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;
using Main.Services.Abstract;

namespace Main.Controllers
{
    public class CrimeController : Controller
    {
        private static readonly int CaliforniaID = 4;

        private readonly IBackendService _backend;
        private readonly ICrimeAPIService _crime;
        private readonly IConfiguration _config;

        public CrimeController(IBackendService backend, ICrimeAPIService cs, IConfiguration config)
        {
            _backend = backend;
            _crime = cs;
            _config = config;

        }

        public IActionResult CityCrimeLookUp()
        {
            return View();
        }

        public IActionResult National()
        {
            return View();
        }

        public IActionResult CrimeStats(string? cityName, int? stateID)
        {
            if (cityName == null || stateID == null)
            {
                cityName = "Riverside";
                stateID = CaliforniaID;
            }

            ViewBag.cityName = cityName;
            ViewBag.stateAbbrev = _backend.GetAllStates()[stateID ?? CaliforniaID].Abbrev;

            return View();
        }

        [HttpGet]
        public IActionResult GetSingleStateStats(string? stateAbbrev, int? aYear)
        {
            if (stateAbbrev == null)
            {
                stateAbbrev = "CA";
            }

            if (aYear == null)
            {
                aYear = 0;
            }

            var state = _crime.GetState(stateAbbrev, aYear);
            return Json(state);
        }

        public IActionResult CheckAnotherYear([Bind("stateAbbrev", "aYear")] StateCrimeViewModel model)
        {
            ViewBag.stateAbbrev = model.stateAbbrev;
            return View();
        }

        [HttpPost]
        public IActionResult FillCheckAnotherYear([Bind("stateAbbrev", "aYear")] StateCrimeViewModel model)
        {
            if (model.stateAbbrev == null)
            {
                model.stateAbbrev = "CA";
            }

            if (model.aYear == null)
            {
                model.aYear = 0;
            }

            var crime = _crime.GetState(model.stateAbbrev, model.aYear);
            return Json(crime);
        }

    }

}