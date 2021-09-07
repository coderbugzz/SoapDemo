using IdentityDemo.Models;
using IdentityDemo.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Controllers
{
    public class AuthController : Controller
    {
        private readonly IRepository _repository;

        public AuthController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = loginViewModel.Email;
                var password = loginViewModel.Password;

                var result = await  _repository.LoginAsync(loginViewModel);
                if (result.code == 200)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, result.Data.Role),
                    new Claim(ClaimTypes.Email,username)};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                   

                    await HttpContext.SignInAsync(
                                              CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(identity),
                                              new AuthenticationProperties
                                              {
                                                  IsPersistent = false   //remember me
                                          });

                    return Redirect("~/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", result.message);
                }
            }

            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.RegisterAsync(registerViewModel);
                if (result.code == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.message);
                }
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");

        }

        public  IActionResult AccessDenied()
        {
            ViewData["errormessage"] = "You are not allowed to access this page!";

            return View();

        }
    }
}
