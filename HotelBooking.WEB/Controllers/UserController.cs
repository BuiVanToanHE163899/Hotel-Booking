using HotelBooking.Domain.Request.Account;
using HotelBooking.Domain.Response.Account;
using HotelBooking.Web.Ultilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HotelBooking.Domain.User;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace HotelBooking.WEB.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginRequest = new LoginRequest()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                LoginResult result = ApiHelper<LoginResult>.HttpPostAsync($"{Helper.ApiUrl}api/account/login", loginRequest);
                if (result.Success)
                {
                    var response = ApiHelper<List<string>>.HttpGetAsync($"{Helper.ApiUrl}api/account/roles/{result.UserId}");
                    List<string> roles = response;
                    string rolesString = string.Join(",", roles);

                    HttpContext.Session.SetString("UserRole", rolesString);
                    HttpContext.Session.SetString("UserName", model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var registerRequest = new RegisterRequest()
                {
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Avatar = model.Avatar
                    
                };
                RegisterResult result = ApiHelper<RegisterResult>.HttpPostAsync($"{Helper.ApiUrl}api/account/register", registerRequest);
                if (result.Success)
                {

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}