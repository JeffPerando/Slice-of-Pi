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

    //public IActionResult StateCrimeStats(string stateAbbrev)
    //{
    //    if (stateAbbrev == null)
    //    {
    //        stateAbbrev = "CA";
    //    }
    //    ViewBag.stateAbbrev = stateAbbrev;
    //    return View();
    //}
    //public IActionResult StateCrimeStats(int? year, string stateAbbrev)
    //{
    //    if (stateAbbrev == null)
    //    {
    //        stateAbbrev = "CA";
    //    }

    //    if (year == null)
    //    {
    //        year = 0;
    //    }
    //    ViewBag.stateAbbrev = stateAbbrev;
        
    //    return View();
    //}

    //[HttpGet]
    //public IActionResult GetStateCrimeStats(int? year, string stateAbbrev)
    //{
    //    if (stateAbbrev == null)
    //    {
    //        stateAbbrev = "CA";
    //    }

    //    if (year == null)
    //    {
    //        year = 0;
    //    }

    //    StateCrimeViewModel state = new StateCrimeViewModel();
    //    _CrimeService.SetCredentials(_config["apiFBIKey"]);
    //    state = _CrimeService.GetState(stateAbbrev, year);
    //    state.aYear = year;
    //    state.stateAbbrev = stateAbbrev;
    //    return Json(state);
    //}


    //[HttpGet]
    //public IActionResult GetCrimeTrends(string cityName, string stateAbbrev)
    //{
    //    if (cityName == null || stateAbbrev == null)
    //    {
    //        cityName = "Riverside";
    //        stateAbbrev = "CA";
    //    }
    //    List<Crime> city_trends = new List<Crime>();
    //    JObject getCitytrends = new JObject();
    //    List<Crime> returnCityTrends = new List<Crime>();

    //    getCitytrends = _CrimeService.GetCityTrends(cityName, stateAbbrev);
    //    returnCityTrends = _CrimeService.ReturnCityTrends(getCitytrends);

    //    return Json(returnCityTrends);
    //}

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
        List<Crime> returnCityTrends = new List<Crime>();

        getCitytrends = _CrimeService.GetCityTrends(cityName, stateAbbrev);
        returnCityTrends = _CrimeService.ReturnCityTrends(getCitytrends);

        return Json(returnCityTrends);
    }
    

}