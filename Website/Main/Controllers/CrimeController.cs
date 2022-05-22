
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
        private readonly ICrimeAPIv2 _crime;
        private readonly IConfiguration _config;

        public CrimeController(ICrimeAPIv2 crime, IConfiguration config)
        {
            _crime = crime;
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

        public IActionResult CrimeStats(string? cityName, string? stateAbbrev)
        {
            if (cityName == null || stateAbbrev == null)
            {
                cityName = "Riverside";
                stateAbbrev = "CA";
            }

            ViewBag.cityName = cityName;
            ViewBag.stateAbbrev = stateAbbrev;

            return View();
        }

    }

}