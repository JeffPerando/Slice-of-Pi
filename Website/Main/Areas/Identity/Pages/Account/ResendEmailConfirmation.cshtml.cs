// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Main.DAL.Abstract;
using Main.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Main.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserVerifier _verifier;

        public ResendEmailConfirmationModel(UserManager<IdentityUser> userManager, IUserVerifier verifier)
        {
            _userManager = userManager;
            _verifier = verifier;
        }

        public async Task<IActionResult> OnGet(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found!");
                return Page();
            }

            _verifier.GenerateVerificationCode(user.Email);

            return RedirectToPage("ConfirmEmail", new { userId = user.Id });
        }
    }
}
