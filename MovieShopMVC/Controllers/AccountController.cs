using ApplicationCore.Models;
using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //show the view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            //user can enter info
            //service, needs to hash the password then save in database
            var user = await _accountService.CreateUser(model);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if (user == null) 
            {
                return View(model);
            }

            //after sucessfull login
            //create cookies
            //cookies have claims
            //claims are user id, first name, last name, email
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.GivenName, user.FirstName)
            };

            //identity object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            //create cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            return LocalRedirect("~/");
            
        }
    }
}
