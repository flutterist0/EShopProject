using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Dto.ServiceDtos;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BrandsController(IBrandService brandService,IUserOperationClaimService userOperationClaimService) : Controller
    {
        private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;
        private readonly IBrandService _brandService = brandService;   
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllBrands()
        {
            try
            {
                int userId = int.Parse(Request.Cookies["userId"] ?? "0");

                var admin = _userOperationClaimService.GetUserOperationClaimsById(userId);
                if (admin.Data.OperationClaimName == "Admin")
                {
                    var result = _brandService.GetAll().Data;
                    return View(result);
                }else
                {
                    return RedirectToAction("AccessDenied", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Dashboard");
            }
        }

        public IActionResult AddBrands()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrands(BrandAddDto brandAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = _brandService.Add(brandAddDto);

                if (result.Success)
                {
                    return RedirectToAction("GetAllBrands", "Brands");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(brandAddDto);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = _brandService.Get(id).Data;
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _brandService.Delete(id);
                if (result.Success)
                {
                    return RedirectToAction("GetAllBrands", "Brands");
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
