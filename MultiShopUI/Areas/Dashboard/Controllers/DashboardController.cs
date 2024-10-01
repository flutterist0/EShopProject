using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    
    public class DashboardController(IAuthService authService,IUserService userService) : Controller
	{
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;
        public IActionResult Index()
		{
            int userId = int.Parse(Request.Cookies["userId"]??"0");
            var user = _userService.GetUserById(userId);

            if (user == null)
            {

                return RedirectToAction("AccessDenied", "Dashboard");
            }

            var isAdmin = _authService.CheckIfUserIsAdmin(userId, user);

           if (isAdmin.Success)
            {
                ViewData["IsAdmin"] = isAdmin.Data;
                return View(); 
            }
            ViewData["IsAdmin"] = isAdmin;
            return RedirectToAction("AccessDenied", "Dashboard"); 
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
       
          
    }


