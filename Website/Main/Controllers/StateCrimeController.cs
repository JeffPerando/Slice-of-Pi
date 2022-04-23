//using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;
//using Newtonsoft.Json.Linq;
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

    public IActionResult StateCrimeStats()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetStateList()
    {
        List<string> state_list = new List<string>();
        state_list = _CrimeService.GetStates();
        return Json(state_list);
    }

}