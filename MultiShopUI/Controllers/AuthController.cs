using Business.Abstract;
using Entities.Dto.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Text.Json;

namespace EShopUI.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;
        [HttpGet]
        public IActionResult Login()
        {
            var cookieValue = Request.Cookies["UserInfo"];

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.Login(loginDto);
                if (result.Success)
                {
                    var user = result.Data;
                    var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim("UserId", user.Id.ToString()) 
                };
                    var claimsIdentity = new ClaimsIdentity(userClaims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    var userData = JsonSerializer.Serialize(user);
                    var userId = user.Id.ToString();
                    var fullName = $"{user.FirstName} {user.LastName}";
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7), 
                        HttpOnly = true,
                        Secure = true
                    };
                    TempData["FullName"] = fullName;
                    Response.Cookies.Append("userId", userId, cookieOptions);
                    Response.Cookies.Append("FullName", fullName, cookieOptions);
                    Response.Cookies.Append("UserInfo", userData, cookieOptions);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(loginDto);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var userExistsResult = _authService.UserExists(registerDto.Email);
                if (!userExistsResult.Success)
                {
                    ModelState.AddModelError("Email", userExistsResult.Message);
                    return View(registerDto);
                }

                if (registerDto.Password != registerDto.RePassword)
                {
                    ModelState.AddModelError("Password", "Shifleler uygun deyil");
                    return View(registerDto);
                }

                var result = _authService.Register(registerDto, registerDto.Password);
                if (result.Success)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(registerDto);
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserInfo");
            Response.Cookies.Delete("FullName");
            Response.Cookies.Delete("userId");
            await  HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
   