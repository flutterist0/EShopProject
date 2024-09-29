using Business.Abstract;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;
using MultiShopUI.Models;
using System.Diagnostics;

namespace MultiShopUI.Controllers
{
	public class HomeController(IServiceService serviceService,ICategoryService categoryService,IBrandService brandService) : Controller
	{
		private readonly IServiceService _serviceService = serviceService;
		private readonly ICategoryService _categoryService = categoryService;
		private readonly IBrandService _brandService = brandService;
		public IActionResult Index()
		{
			try
			{
				HomeVM vm = new()
				{
					GetServices = _serviceService.GetAll().Data,
					GetCategoriesIsFeatured = _categoryService.GetAllIsFeatured().Data,
					GetBrands = _brandService.GetAll().Data
				};
                return View(vm);
            }
			catch (Exception ex)
			{
                return View(ex);
            }
		}
	}
}
