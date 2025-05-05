using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    
    public class DashboardController(IAuthService authService,IUserService userService,IUserOperationClaimService userOperationClaimService) : Controller
	{
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;
        private IUserOperationClaimService _userOperationClaimService = userOperationClaimService;
        public IActionResult Index()
		{
            int userId = int.Parse(Request.Cookies["userId"]??"0");
            var operatioClaim = _userOperationClaimService.GetUserOperationClaimsById(userId);
            ViewData["admin"] = operatioClaim.Data.OperationClaimName;
            Console.WriteLine(ViewData["admin"]);
            var user = _userService.GetUserById(userId);
            if (user == null)
            {

                return RedirectToAction("AccessDenied", "Dashboard");
            }

           var isAdmin = _authService.CheckIfUserIsAdmin(userId, user);

           if (isAdmin.Success)
            {
                var isAdmin1 = isAdmin.Data.ToString();

                HttpContext.Session.SetString("IsAdmin",isAdmin1);

                return View(); 
            }

            TempData["IsAdmin"] = isAdmin.Data;
            return RedirectToAction("AccessDenied", "Dashboard"); 
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
       
          
    }


