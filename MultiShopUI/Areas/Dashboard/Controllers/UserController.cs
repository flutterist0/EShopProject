using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class UserController(IUserService userService,IUserOperationClaimService userOperationClaimService,IOperationClaimService operationClaimService) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService = operationClaimService;
        public IActionResult User()
        {
            try
            {
                var result = _userService.GetUsersWithOperationClaim();
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Dashboard");
            }
        }

        public IActionResult AddOperationClaim()
        {
            ViewBag.OperationClaims = new SelectList(_operationClaimService.GetAll().Data, "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().Data, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        public IActionResult AddOperationClaim(int userId,int operationClaimId)
        {
            var result = _userOperationClaimService.Add(userId, operationClaimId);
            if (ModelState.IsValid) 
            {
                if (result.Success)
                {
                   return RedirectToAction("User","User");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            ViewBag.OperationClaims = new SelectList(_operationClaimService.GetAll().Data, "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().Data, "Id", "Name");
            return View("Error");
            
        }
      
        public IActionResult DeleteConfirmed(int userId,int operationClaimId)
        {
            try
            {
                var result = _userOperationClaimService.Delete(userId,operationClaimId);
                if (result.Success)
                {
                    return RedirectToAction("User", "User");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
