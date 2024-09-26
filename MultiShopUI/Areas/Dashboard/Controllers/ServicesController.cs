using Business.Abstract;
using Entities.Concrete;
using Entities.Dto.ProductDtos;
using Entities.Dto.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]

    public class ServicesController(IServiceService serviceService) : Controller
    {
        private readonly IServiceService _serviceService = serviceService;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllServices()
        {
            try
            {
                var result = _serviceService.GetAll().Data;
                return View(result);
            }
            catch (Exception ex)
            {
                return View(ex);
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
            var service = _serviceService.Get(id); // Xidmət məlumatlarını gətirin
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
                ImageUrl = service.Data.ImageUrl // Burada şəkil URL-sini əlavə edin
            };

            return View(serviceUpdateDto);
        }

        [HttpPost("editService")]
        public IActionResult UpdateService(ServiceUpdateDto service,int id)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }

            var result = _serviceService.Update(service, id);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(service);
            }

            return RedirectToAction("GetAllServices", "Services");
        }
    }
}
