using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.DAL.Abstract;


namespace Main.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrimeAPIService _CrimeService;
    private readonly IConfiguration _config;

    public HomeController(ILogger<HomeController> logger, ICrimeAPIService cs, IConfiguration config)
    {
        _logger = logger;
        _CrimeService = cs;
        _config = config;

    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult GetSafestState()
    {
        List<string> state_list = new List<string>();
        List<Crime> top_five_states = new List<Crime>();
        List<Crime> get_national_stats = new List<Crime>();;

        _CrimeService.SetCredentials(_config["apiFBIKey"]);
        state_list = _CrimeService.GetStates();
        get_national_stats = _CrimeService.ReturnStateCrimeList(state_list);
        top_five_states = _CrimeService.GetSafestStates(get_national_stats);

        return Json(top_five_states);
    }
    [HttpGet]
    public IActionResult GetListStates()
    {
        List<string> state_list = new List<string>();
        state_list = _CrimeService.GetStates();
        return Json(state_list);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
