
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

    }

}