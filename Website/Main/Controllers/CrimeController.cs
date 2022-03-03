using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;


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

    public IActionResult newIndex()
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

        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        getCityStats = _CrimeService.GetCityStats(cityName, stateAbbrev);
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

    public IActionResult SingleStateStats(string stateAbbrev)
    {
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }
        ViewBag.stateAbbrev = stateAbbrev;
        return View();
    }

    [HttpGet]
    public IActionResult GetSingleStateStats(string stateAbbrev)
    {
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }

        stateCrimeViewModel state = new stateCrimeViewModel();
        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        state =_CrimeService.GetState(stateAbbrev);
        return Json(state);
    }

    [HttpGet]
    public IActionResult GetStateList()
    {
        List<string> state_list = new List<string>();
        state_list = _CrimeService.GetStates();
        return Json(state_list);
    }



}