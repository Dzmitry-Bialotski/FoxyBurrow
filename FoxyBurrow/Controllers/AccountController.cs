using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Util.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMailService _mailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
                 RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IMailService mailService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mailService = mailService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            //await _mailService.SendToGmailConfirmLinkAsync("dimasiandro@gmail.com","тестим", "https://localhost:44302/Account/ConfirmEmail?userId=9073e1f0-2478-499a-8932-e5ed1db82da1&token=CfDJ8LD96Ep0jLBMk%2FQuDHZF9pymSHo0%2FP9ddP6okgaYaNmKg4skkO8AX0ap41iX46TTgNBZlAmdAGR9a4p9%2FhhefVod%2Fhw1CsbU3Ayh3tPWxTM83bh%2BmYCMVpvOthvgidRUuXBL4K%2FMdaRv0aIJxjv11E2RnYZNCpporUY0mBMRT7J8fQcptkOBgkiDoYhenfvw4wB7bgfELthWqbSnlFDv%2BmydNNPLcNqolEoI5RZG5ikS7hRjlSy3GAGay6N00vc1Jw%3D%3D");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserInformation uinfo = new UserInformation { 
                    FirstName = model.Name, SecondName = model.Surname };
                User user = new User { Email = model.Email, UserName = model.Email, UserInformation = uinfo };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = 
                        Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);


                    //_logger.LogInformation(confirmationLink);
                    await _mailService.SendToGmailConfirmLinkAsync(model.Email, "Email Confirm to FoxyBurrow",
                        confirmationLink);


                    _logger.LogInformation($"user {model.Email} register");
                    await _userManager.AddToRoleAsync(user, "user");

                    ViewBag.Title = "Registrarion successful";
                    ViewBag.Message = "Before you can Login, please confirm your " +
                        "email, by clicking on the confirmation link we have emailed you";
                    return View("ConfirmEmailInformation");
                    //await _signInManager.SignInAsync(user, false);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email is not confirmed yet");
                    return View(model);
                }
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"user {model.Email} log in");
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                    new { ReturnUrl = returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(
            string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager
                    .GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if(remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                _logger.LogError($"Error from external provider: {remoteError}");
                return View("Login", model);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if(info ==null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                _logger.LogError("Error loading external login information.");

                return View("Login", model); 
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                        info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if(signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if(email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if(user == null)
                    {
                        var uinfo = new UserInformation()
                        {
                            FirstName = "Unknow",
                            SecondName = "Foxy"
                        };
                        user = new User()
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserInformation = uinfo
                        };
                        await _userManager.CreateAsync(user);
                        await _userManager.AddToRoleAsync(user, "user");
                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                return View("/Error/{0}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                RedirectToAction("index", "home");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "Email can not be confirmed :(";
            return View("Error");
        }
    }
}
