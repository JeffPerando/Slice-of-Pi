// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Main.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserVerifierService _verifier;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, IUserVerifierService verifier)
        {
            _userManager = userManager;
            _verifier = verifier;

        }

        public string Email { get; set; }
        public bool Confirmed { get; set; }
        public string ResendEmailLink { get; set; }
        public List<string> Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null)
            {
                return Redirect("/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            Email = user.Email;
            Confirmed = false;
            ResendEmailLink = Url.Page("/Account/SendEmailConfirmation",
                pageHandler: null,
                values: new { userId = userId },
                protocol: Request.Scheme);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string email = Request.Form["email"];
            string code = Request.Form["code"];

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return RedirectPermanent("Index");
            }

            if (user.EmailConfirmed)
            {
                return Redirect("Login");
            }

            int parsedCode;
            Int32.TryParse(code, out parsedCode);
            Email = email;
            Confirmed = _verifier.Verify(email, parsedCode);
            ResendEmailLink = Url.Page("Account/ResendEmailConfirmation",
                pageHandler: null,
                values: new { userId = user.Id },
                protocol: Request.Scheme);

            if (Confirmed)
            {
                //workaround for not being able to set the confirmed flag manually
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);
            }
            else
            {
                ViewData["Message"] = "Could not verify email code! Try resending it.";
            }

            return Page();
        }

    }
}
