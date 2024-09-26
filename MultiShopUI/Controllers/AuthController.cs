using Business.Abstract;
using Entities.Dto.Auth;
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
                    new Claim("UserId", user.Id.ToString()) // Eğer UserId varsa ekleyin
                };
                    var claimsIdentity = new ClaimsIdentity(userClaims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    // Kullanıcı bilgilerini JSON formatında serialize et
                    var userData = JsonSerializer.Serialize(user);
                    var fullName = $"{user.FirstName} {user.LastName}";
                    // Cookie ayarları
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7), // Cookie'nin geçerlilik süresi
                        HttpOnly = true, // JavaScript'ten erişimi engelle
                        Secure = true // Sadece HTTPS üzerinden gönderilsin
                    };
                    //ViewBag.FullName = fullName;
                    TempData["FullName"] = fullName;
                    // Cookie oluştur
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

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı zaten var mı kontrol et
                var userExistsResult = _authService.UserExists(registerDto.Email);
                if (!userExistsResult.Success)
                {
                    ModelState.AddModelError("Email", userExistsResult.Message);
                    return View(registerDto);
                }

                // Şifreyi doğrula
                if (registerDto.Password != registerDto.RePassword)
                {
                    ModelState.AddModelError("Password", "Şifreler eşleşmiyor.");
                    return View(registerDto);
                }

                var result = _authService.Register(registerDto, registerDto.Password);
                if (result.Success)
                {
                    // Kayıt başarılı, kullanıcıyı yönlendir
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(registerDto);
        }

        
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserInfo");

            return RedirectToAction("Login","Auth");
        }
    }
}
