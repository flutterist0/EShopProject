using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoriesController(ICategoryService categoryService,IUserOperationClaimService userOperationClaimService) : Controller
    {
        private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;

        private readonly ICategoryService _categoryService = categoryService;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllCategories()
        {
            try
            {
                int userId = int.Parse(Request.Cookies["userId"] ?? "0");

                var admin = _userOperationClaimService.GetUserOperationClaimsById(userId);
                if (admin.Data.OperationClaimName == "Admin")
                {
                    var result = _categoryService.GetAll().Data;
                    return View(result);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Dashboard");
                }
            
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Dashboard");
            }
        }
        public IActionResult AddCategories()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategories(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(categoryAddDto);

                if (result.Success)
                {
                    return RedirectToAction("GetAllCategories", "Categories");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(categoryAddDto);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.Get(id).Data;
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _categoryService.Delete(id);
                if (result.Success)
                {
                    return RedirectToAction("GetAllCategories", "Categories");
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
