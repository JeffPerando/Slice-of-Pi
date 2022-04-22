using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.Models.Listings;
using Main.DAL.Abstract;
//using Main.DAL.Concrete;
//using Newtonsoft.Json.Linq;

namespace Main.Controllers;

public class ATTOMController : Controller
{
    private readonly ILogger<ATTOMController> _logger;
    private readonly ICrimeAPIService _CrimeService;
    private readonly IConfiguration _config;
    private readonly IHousingAPI _ATTOMService;

    public ATTOMController(ILogger<ATTOMController> logger, ICrimeAPIService cs, IConfiguration config, IHousingAPI attom)
    {
        _logger = logger;
        _CrimeService = cs;
        _config = config;
        _ATTOMService = attom;
    }

    public IActionResult Listings()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Listings(string zipcode, string pages, string minPrice, string maxPrice, string orderBy)
    {
        AttomJson data = new AttomJson();
        //data = _ATTOMService.GetListing(zipcode, pages, minPrice, maxPrice, orderBy);

        return View();// View(data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
