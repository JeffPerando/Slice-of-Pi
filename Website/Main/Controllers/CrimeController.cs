
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;

namespace Main.Controllers;

public class CrimeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrimeAPIService _crime;
    private readonly IConfiguration _config;

    public CrimeController(ILogger<HomeController> logger, ICrimeAPIService cs, IConfiguration config)
    {
        _logger = logger;
        _crime = cs;
        _config = config;

    }

    public IActionResult National()
    {
        return View();
    }

    public IActionResult CrimeStats(string cityName, string stateAbbrev)
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
    
    public IActionResult CityCrimeLookUp(string? stateAbbrev)
    {
        return View();
    }

    public IActionResult StateCrimeStats(string? stateAbbrev)
    {
        return View();
    }

    public IActionResult SingleStateStats(string? stateAbbrev)
    {
        ViewBag.stateAbbrev = stateAbbrev ?? "CA";

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