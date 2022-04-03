using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;

namespace Main.Controllers;

public class StateCrimeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrimeAPIService _CrimeService;
    private readonly CrimeDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public StateCrimeController(ILogger<HomeController> logger, ICrimeAPIService cs, CrimeDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _CrimeService = cs;
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;

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
        /*
        if (stateAbbrev == null)
        {
            stateAbbrev = "CA";
        }

        if (year == null)
        {
            year = 0;
        }
        */
        stateAbbrev = TempData["stuff"].ToString();
        year = Convert.ToInt32(TempData["moreStuff"]);

        StateCrimeViewModel state = new StateCrimeViewModel();
        state = _CrimeService.GetState(stateAbbrev, year);
        
        if (_signInManager.IsSignedIn(User))
        {
            var result = new StateCrimeSearchResult();

            result.UserId = _userManager.GetUserId(User);
            result.DateSearched = DateTime.Now;
            result.State = stateAbbrev;
            result.Year = year ?? 0;

        }

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