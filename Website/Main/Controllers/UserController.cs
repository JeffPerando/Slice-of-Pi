
using Main.DAL.Abstract;
using Main.Models;
using Main.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class UserController : Controller
    {
        private readonly ISiteUserService _users;
        private readonly IHousePriceCalcService _housePriceCalc;
        private readonly CrimeDbContext _db;

        public UserController(ISiteUserService users, IHousePriceCalcService housePriceCalc, CrimeDbContext db)
        {
            _users = users;
            _housePriceCalc = housePriceCalc;
            _db = db;

        }

        [HttpGet]
        public IActionResult Edit()
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            return View(_users.Data(User));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User form)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            var user = _users.Data(User);
            var msgs = new List<string>();

            if (user.Name != form.Name && !string.IsNullOrEmpty(form.Name))
            {
                user.Name = form.Name.Trim();
                msgs.Add("Name successfully changed!");
            }

            if (user.EmailAddress != form.EmailAddress && !string.IsNullOrEmpty(form.EmailAddress))
            {
                user.EmailAddress = form.EmailAddress.Trim();
                msgs.Add("Email successfully changed!");
            }

            if (msgs.Count() == 0)
            {
                msgs.Add("Nothing updated");
            }
            else
            {
                _db.Users.Update(user);
                _db.SaveChangesAsync();
            }

            ViewData["Messages"] = string.Join("\n", msgs);

            return View(user);
        }

        [HttpGet]
        public IActionResult Addresses()
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            return View(_users.Addresses(User));
        }

        [HttpPost]
        //The POST adds an address to the current user
        public IActionResult Addresses(string street, string city, string state, string zip)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            _users.AddAddress(User, new Home { StreetAddress = street, County = city, State = state, ZipCode = zip });

            var id = _users.ID(User);

            ViewData["Message"] = "Address added successfully!";

            return View(_users.Addresses(User));
        }

        [HttpGet]
        public IActionResult DeleteAddress(int? id)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            var uid = _users.ID(User);

            if (id != null)
            {
                var home = _db.Homes.Where((h) => h.Id == id && h.UserId == uid).FirstOrDefault();

                if (home != null)
                {
                    _db.Homes.Remove(home);
                    _db.SaveChanges();

                }

            }

            return RedirectToAction("Addresses", "User");
        }

        public IActionResult Searches()
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Assessments()
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            List<WeightedAssessment> assms = new();

            var addresses = _users.Addresses(User);

            foreach (var addr in addresses)
            {
                assms.Add(_housePriceCalc.CalcCrimeWeightAssessment(addr));

            }

            return View(assms);
        }

        [HttpPost]
        public IActionResult Assessments(string street, string city, string state, string zip)
        {
            if (!_users.IsLoggedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            _users.AddAddress(User, new Home { StreetAddress = street, County = city, State = state, ZipCode = zip });

            return Assessments();
        }

    }

}
