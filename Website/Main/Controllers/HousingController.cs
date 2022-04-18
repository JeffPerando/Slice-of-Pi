
using Main.DAL.Abstract;
using Main.Models;
using Main.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class HousingController : Controller
    {
        private readonly ISiteUserService _users;
        
        public HousingController(ISiteUserService users)
        {
            _users = users;
            
        }

    }

}
