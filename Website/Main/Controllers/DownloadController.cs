
using Main.DAL.Abstract;
using Main.Helpers;
using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Main.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ISiteUserService _users;
        
        public DownloadController(ISiteUserService users)
        {
            _users = users;

        }

        public IActionResult StateCrimeSearchHistory()
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            var results = _users.StateCrimeSearchResults(User);

            if (results != null)
            {
                var csv = CSVParseHelper.fromStateSearchHistory(results);
                return File(Encoding.UTF8.GetBytes(csv), "application/octet", "StateCrimeSearchHistory.csv");
            }

            return NotFound();
        }

    }

}
