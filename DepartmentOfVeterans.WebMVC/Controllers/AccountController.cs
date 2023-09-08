using DepartmentOfVeterans.WebMVC.Models;
using DepartmentOfVeterans.WebMVC.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DepartmentOfVeterans.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ViewResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var token = await _authenticationService.Authenticate(login.Email, login.Password);
                if (token != string.Empty)
                {
                    //  store token and current email to cookies
                    Response.Cookies.Append("X-Access-Token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });

                    Response.Cookies.Append("CurrentUserEmail", login.Email, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });

                    return RedirectToAction("Index", "User");
                }
            }
            return View(login);
        }

        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel login)
        {
            if (ModelState.IsValid)
            {
                var response = await _authenticationService.Register(login.FirstName, login.LastName, login.UserName, login.Email, login.Password);

                if (response.Success)
                {
                    return RedirectToAction("Login");    // return to Login screen
                }
                else
                {
                    ViewBag.Message = response.Message;
                    ModelState.AddModelError("Error", response.Message);
                }
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login");
        }
    }
}
