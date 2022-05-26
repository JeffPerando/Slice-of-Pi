
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using Main.Models.Listings;
using Main.DAL.Abstract;

namespace Main.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ATTOMController : Controller
    {
        private readonly ILogger<ATTOMController> _logger;
        private readonly IConfiguration _config;
        private readonly IHousingAPI _housing;
        private readonly ISiteUserService _users;
        private readonly IGoogleStreetViewAPIService _googleStreetViewAPIService;

        public ATTOMController(ILogger<ATTOMController> logger, IConfiguration config, IHousingAPI attom,
            IGoogleStreetViewAPIService googleStreetViewAPIService, ISiteUserService users)
        {
            _logger = logger;
            _config = config;
            _housing = attom;
            _users = users;
            _googleStreetViewAPIService = googleStreetViewAPIService;
        }

        public IActionResult Listings()
        {
            return View();
        }

        public IActionResult StreetViewLookUp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listings(string zipcode, string pages, string minPrice, string maxPrice, string orderBy)
        {
            //AttomJson data = new AttomJson();
            var data = _housing.GetListing(zipcode, pages, minPrice, maxPrice, orderBy);

            return View(data);
        }

        [HttpGet]
        public IActionResult StreetView(string streetAddress, string cityName, string stateAbbrev)//Need more research as to how I can display the picture. Might be able to just place the url
        {//in the html and display it that way

        StreetViewViewModel model = new StreetViewViewModel();
        StreetViewViewModel temp = new StreetViewViewModel();
        var x = _googleStreetViewAPIService.GetStreetView(streetAddress);
       
        if (cityName == null && stateAbbrev == null)
        {
            temp = _googleStreetViewAPIService.ParseAddressSubmission(streetAddress);
            cityName = _googleStreetViewAPIService.ToUpperCase(temp.CityName);
            streetAddress = temp.Address;
            stateAbbrev = temp.StateName;
        }
        
        ViewBag.streetAddress = streetAddress;
        ViewBag.stateAbbrev = stateAbbrev;
        ViewBag.cityName = cityName;
        model.Akey = _googleStreetViewAPIService.GetEmbededMap(streetAddress + " " + cityName + " " + stateAbbrev);
        var address2 = cityName + ", " + stateAbbrev;
        var house_info = _housing.GetHouseInformation(streetAddress, address2);

            model.Address = x;
            return View(Tuple.Create(model, house_info));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}