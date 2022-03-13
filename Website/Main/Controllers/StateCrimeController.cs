using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;

namespace Main.Controllers;

public class StateCrimeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrimeAPIService _CrimeService;
    private readonly IConfiguration _config;

    public StateCrimeController(ILogger<HomeController> logger, ICrimeAPIService cs, IConfiguration config)
    {
        _logger = logger;
        _CrimeService = cs;
        _config = config;

    }

    public IActionResult StateCrimeStats(int? year, string stateAbbrev, StateCrimeViewModel model)
    {

        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }

        if (year == null)
        {
            year = 0;
        }
        TempData["stuff"] = stateAbbrev;
        TempData["moreStuff"] = year;
        ViewBag.stateAbbrev = stateAbbrev;
        ViewBag.year = year;
        return View();
    }

    [HttpGet]
    public IActionResult GetStateCrimeStats(int? year, string stateAbbrev, StateCrimeViewModel model)
    {
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }

        if (year == null)
        {
            year = 0;
        }
        stateAbbrev = TempData["stuff"].ToString();
        year = Convert.ToInt32(TempData["moreStuff"]);
        StateCrimeViewModel state = new StateCrimeViewModel();
        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        state = _CrimeService.GetState(stateAbbrev, year);
 
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