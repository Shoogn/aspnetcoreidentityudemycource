using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UdemyCource.Models;
using UdemyCource.ViewModels;

namespace UdemyCource.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = registerModel.UserEmail,
                    PhoneNumber = registerModel.PhoneNumber,
                    UserName = registerModel.UserEmail,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    UserAge = registerModel.UserAge,
                };

                var identityResult = await _userManager.CreateAsync(user, registerModel.Password);

                if (identityResult.Succeeded)
                {
                  
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.HomePhone, "019-1236985", ClaimValueTypes.String),
                        new Claim("CustomClaim", user.Email, ClaimValueTypes.String)
                    };

                    var identityClaimResult = await _userManager.AddClaimsAsync(user, userClaims);

                    if (identityClaimResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }


                return View(registerModel);
            }
            return View(registerModel);
        }

        [AllowAnonymous]
        public IActionResult SignInUser(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl ?? "/";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignInUser(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user == null)
                {
                    ViewBag.returnUrl = returnUrl ?? "/";
                    return View(loginModel);
                }
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false);
                return LocalRedirect(returnUrl);
            }

            ViewBag.returnUrl = returnUrl ?? "/";
            return View(loginModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
