﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.DAL.Models;
using WebShopApp.Infrastructure.Interface;
using WebShopApp.Infrastructure.Services;
using WebShopApp.Models.Account;

namespace WebShopApp.Controllers.Account
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger logger;

        public AccountController(
            IRepository repository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.repository = repository;
            this.logger = logger;
        }

        #region LOGIN

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            LoginViewModel model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.IsUserActive == false)
                {
                    ModelState.AddModelError("", "This account is not active. Please contact support.");
                    return View(model);
                }
            }

            if (model.Password == null || model.Password == "")
            {
                ModelState.AddModelError("", "Password is required.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                repository.Update<ApplicationUser>(user);
                await repository.SaveAsync();

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ModelState.AddModelError("", "Wrong email or password.");
                return View(model);
            }
        }
        #endregion

        #region LOGOUT

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion

        #region REGISTER

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel();
            return View(register);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsUserActive = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await repository.SaveAsync();

                return Redirect("/Account/Login");
            }
            else
            {
                AddErrors(result);
                ModelState.AddModelError("", "An error occured during registraion process.");
                return View(model);
            }
        }

        #endregion

        #region CHANGE PASSWORD
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel changePassword = new ChangePasswordViewModel();
            return View(changePassword);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["success"] = "Password successfuly changed!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                    ModelState.AddModelError("", "Error occurred while changing the password.");
                    return View(model);
                }
            }

            return View(model);
        }

        #endregion

        #region PRIVATE 
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }

}
