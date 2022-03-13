
using Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CrimeDbContext _db;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CrimeDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        private async Task<IdentityUser?> ConfirmLoginAsync()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return null;
            }

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (!user.EmailConfirmed)
            {
                return null;
            }

            return user;
        }

        private async Task<User?> GetCrimeUserAsync()
        {
            var user = await ConfirmLoginAsync();

            if (user == null)
            {
                return null;
            }

            var crimeUser = _db.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (crimeUser == null)
            {
                crimeUser = new User();
                crimeUser.Id = user.Id;
                crimeUser.Name = "J. Doe";
                crimeUser.EmailAddress = user.Email;
                crimeUser.Address = "";

                _db.Users.Add(crimeUser);
                _db.SaveChanges();

            }

            return crimeUser;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await GetCrimeUserAsync();

            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User form)
        {
            var user = await GetCrimeUserAsync();

            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var msgs = new List<string>();

            if (user.Name != form.Name && !string.IsNullOrEmpty(form.Name))
            {
                user.Name = form.Name;
                msgs.Add("Name successfully changed!");
            }

            if (user.EmailAddress != form.EmailAddress && !string.IsNullOrEmpty(form.EmailAddress))
            {
                user.EmailAddress = form.EmailAddress;
                msgs.Add("Email successfully changed!");
            }

            if (user.Address != form.Address && !string.IsNullOrEmpty(form.Address))
            {
                user.Address = form.Address;
                msgs.Add("Address updated successfully!");
            }

            if (msgs.Count() == 0)
            {
                msgs.Add("Nothing updated");
            }
            else
            {
                _db.Users.Update(user);
                _db.SaveChanges();
            }

            ViewData["Messages"] = string.Join("\n", msgs);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Addresses()
        {
            var user = await GetCrimeUserAsync();

            if (user == null)
            {
                return Redirect("/");
            }

            var homes = _db.Homes.Where((h) => h.UserId == user.Id).ToList();

            return View(homes);
        }

        [HttpPost]
        //The POST adds an address to the current user
        public async Task<IActionResult> Addresses(string street, string city, string state, string zip)
        {
            var user = await GetCrimeUserAsync();

            if (user == null)
            {
                return Redirect("/");
            }

            var newHome = new Home
            {
                StreetAddress = street,
                County = city,
                State = state,
                ZipCode = zip,
                UserId = user.Id
            };

            _db.Homes.Add(newHome);
            _db.SaveChanges();

            var homes = _db.Homes.Where((h) => h.UserId == user.Id).ToList();

            ViewData["Message"] = "Address added successfully!";

            return View(homes);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAddress(int? id)
        {
            var user = await GetCrimeUserAsync();

            if (user == null)
            {
                return Redirect("/");
            }

            if (id != null)
            {
                var home = _db.Homes.Where((h) => h.Id == id && h.UserId == user.Id).FirstOrDefault();

                if (home != null)
                {
                    _db.Homes.Remove(home);
                    _db.SaveChanges();
                }

            }

            return RedirectToAction("Addresses", "User");
        }

    }

}
