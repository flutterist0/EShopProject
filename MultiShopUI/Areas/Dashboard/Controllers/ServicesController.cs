using Business.Abstract;
using Entities.Concrete;
using Entities.Dto.ProductDtos;
using Entities.Dto.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]

    public class ServicesController(IServiceService serviceService, IUserOperationClaimService userOperationClaimService) : Controller
    {
        private readonly IServiceService _serviceService = serviceService;
        private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllServices()
        {
            try
            {
                int userId = int.Parse(Request.Cookies["userId"] ?? "0");

                var admin = _userOperationClaimService.GetUserOperationClaimsById(userId);
                if (admin.Data.OperationClaimName == "Admin")
                {
                    var result = _serviceService.GetAll().Data;
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

        public IActionResult AddServices()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddServices(ServiceAddDto serviceAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = _serviceService.Add(serviceAddDto);

                if (result.Success)
                {
                    return RedirectToAction("GetAllServices", "Services");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(serviceAddDto);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = _serviceService.Get(id).Data;
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _serviceService.Delete(id);
                if (result.Success)
                {
                    return RedirectToAction("GetAllServices", "Services");
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
        [HttpGet("editService/{id}")]
        public IActionResult EditService(int id)
        {
            var service = _serviceService.Get(id);
            if (service == null)
            {
                return NotFound();
            }

            var serviceUpdateDto = new ServiceUpdateDto
            {
                ServiceId = service.Data.Id,
                Title = service.Data.Title,
                Description = service.Data.Description,
                IsFeatured = service.Data.IsFeatured,
                ImageUrl = service.Data.ImageUrl 
            };

            return View(serviceUpdateDto);
        }

        //[HttpPost("editService")]
        //public IActionResult UpdateService(ServiceUpdateDto service,int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(service);
        //    }

        //    var result = _serviceService.Update(service, id);

        //    if (!result.Success)
        //    {
        //        ModelState.AddModelError("", result.Message);
        //        return View(service);
        //    }

        //    return RedirectToAction("GetAllServices", "Services");
        //}
    }
}
