﻿using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MiniStackOverflow.Application.Utilities;
using MiniStackOverflow.Infrastructure.Membership;
using MiniStackOverflow.Web.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace MiniStackOverflow.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthController(ILifetimeScope scope,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthController> logger,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _scope = scope;
            _emailService = emailService;
        }

        public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<RegistrationModel>();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegistrationModel model)
        {
            model.ReturnUrl ??= Url.Content("~/Auth/Login");

            if (ModelState.IsValid)
            {
                var at = model.Email.IndexOf('@');
                var userName = model.Email.Substring(0, at);

                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddClaimAsync(user, new Claim("PostQuestion", "true"));
                await _userManager.AddClaimAsync(user, new Claim("PostAnswer", "true"));

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.ActionLink(
                        action: "ConfirmEmail",
                        controller: "Account",
                        values: new { userId = user.Id, code, returnUrl = model.ReturnUrl },
                        protocol: Request.Scheme);


                    await _emailService.SendSingleEmail(model.FirstName + " " + model.LastName, model.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", "Account", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = _scope.Resolve<LoginModel>();
            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.IsNotAllowed)
                {
                    return RedirectToAction("RegisterConfirmation", "Account", new { email = model.Email });

                }

                if (result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();

                    var userClaim = await _userManager.GetClaimsAsync(user);

                    model.ReturnUrl ??= Url.Content("~/");

                    return RedirectToAction("Index", "Question");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
