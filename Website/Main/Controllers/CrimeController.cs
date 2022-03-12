
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;

namespace Main.Controllers;

public class CrimeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrimeAPIService _CrimeService;
    private readonly IConfiguration _config;

    public CrimeController(ILogger<HomeController> logger, ICrimeAPIService cs, IConfiguration config)
    {
        _logger = logger;
        _CrimeService = cs;
        _config = config;

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
    
    [HttpGet]
    public IActionResult GetCrimeStats(string cityName, string stateAbbrev)
    {
        if (cityName == null || stateAbbrev == null)
        {
            cityName = "Riverside";
            stateAbbrev = "CA";
        }
        
        
        List<Crime> city_stats = new List<Crime>();
        List<Crime> getCityStats = new List<Crime>();

        getCityStats = _CrimeService.GetCityStats(cityName, stateAbbrev);
        city_stats = _CrimeService.ReturnCityStats(getCityStats);

        return Json(city_stats);
    }

    [HttpGet]
    public IActionResult UpdateCrimeStats(string cityName, string stateAbbrev, string year)
    {
        List<Crime> city_stats = new List<Crime>();
        List<Crime> getCityStats = new List<Crime>();

        getCityStats = _CrimeService.GetCityStatsByYear(cityName, stateAbbrev, year);
        city_stats = _CrimeService.ReturnCityStats(getCityStats);

        return Json(city_stats);
    }


    public IActionResult StateCrimeStats(string stateAbbrev)
    {
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }
        ViewBag.stateAbbrev = stateAbbrev;
        return View();
    }

    public IActionResult SingleStateStats(string stateAbbrev, [Bind("stateAbbrev", "aYear")] StateCrimeViewModel model)
    {
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }
        ViewBag.stateAbbrev = stateAbbrev;
        model.stateAbbrev = stateAbbrev;
        return View(model);
    }

    [HttpGet]
    public IActionResult GetSingleStateStats([Bind("stateAbbrev", "aYear")] StateCrimeViewModel model)
    {
        if (model.stateAbbrev == null)
        {
            model.stateAbbrev = "CA";
        }

        if (model.aYear == null)
        {
            model.aYear = 0;
        }

        StateCrimeViewModel state = new StateCrimeViewModel();
        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        state = _CrimeService.GetState(model.stateAbbrev, model.aYear);
        state.aYear = model.aYear;
        state.stateAbbrev = model.stateAbbrev;
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

        StateCrimeViewModel state = new StateCrimeViewModel();
        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        
        state =_CrimeService.GetState(model.stateAbbrev, model.aYear);
        state.aYear = model.aYear;
        state.stateAbbrev = model.stateAbbrev;
        return Json(state);
    }

    [HttpGet]
    public IActionResult GetStateList()
    {
        List<string> state_list = new List<string>();
        state_list = _CrimeService.GetStates();
        return Json(state_list);
    }


    [HttpGet]
    public IActionResult GetCrimeTrends(string cityName, string stateAbbrev)
    {
        if (cityName == null || stateAbbrev == null)
        {
            cityName = "Riverside";
            stateAbbrev = "CA";
        }
        List<Crime> city_trends = new List<Crime>();
        JObject getCitytrends = new JObject();
        List<Crime> returnTotalCityTrends = new List<Crime>();
        List<Crime> returnPropertyCityTrends = new List<Crime>();
        List<Crime> returnViolentCityTrends = new List<Crime>();

        getCitytrends = _CrimeService.GetCityTrends(cityName, stateAbbrev);
        returnTotalCityTrends = _CrimeService.ReturnTotalCityTrends(getCitytrends);
        returnPropertyCityTrends = _CrimeService.ReturnPropertyCityTrends(getCitytrends);
        returnViolentCityTrends = _CrimeService.ReturnViolentCityTrends(getCitytrends);
        

        return Json(new {totalTrends = returnTotalCityTrends, propertyTrends = returnPropertyCityTrends, violentTrends = returnViolentCityTrends});
    }
    
}