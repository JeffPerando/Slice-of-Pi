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

    public IActionResult StateCrimeStats(int? year, string stateAbbrev)
    {
        ViewBag.stateAbbrev = stateAbbrev ?? "CA";
        ViewBag.year = year ?? 0;
        
        return View();
    }

    [HttpGet]
    public IActionResult GetStateCrimeStats(int? year, string? stateAbbrev)
    {
        string state = stateAbbrev ?? "CA";
        int actualYear = year ?? 2020;

        var result = _CrimeService.GetState(state, actualYear);
        
        if (_signInManager.IsSignedIn(User))
        {
            result.UserId = _userManager.GetUserId(User);
            result.DateSearched = DateTime.Now;
            
            _db.StateCrimeSearchResults.Add(result);
            _db.SaveChangesAsync();

        }

        return Json(result);
    }

    [HttpGet]
    public IActionResult GetStateList()
    {
        List<string> state_list = new List<string>();
        state_list = _CrimeService.GetStates();
        return Json(state_list);
    }



}