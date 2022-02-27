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

    [HttpGet]
    public IActionResult GetCrimeTrends(string cityName, string stateAbbrev)
    {
        if (cityName == null || stateAbbrev == null)
        {
            cityName = "Riverside";
            stateAbbrev = "CA";
        }
        List<Crime> city_trends = new List<Crime>();
        List<Crime> getCitytrends = new List<Crime>();

        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        getCitytrends = _CrimeService.GetCityTrends(cityName, stateAbbrev);
        
        return Json(getCitytrends);
    }
    

}