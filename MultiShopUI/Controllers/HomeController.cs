using Business.Abstract;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;
using MultiShopUI.Models;
using System.Diagnostics;

namespace MultiShopUI.Controllers
{
	public class HomeController(IServiceService serviceService,ICategoryService categoryService,IBrandService brandService,IProductService productService) : Controller
	{
		private readonly IServiceService _serviceService = serviceService;
		private readonly ICategoryService _categoryService = categoryService;
		private readonly IBrandService _brandService = brandService;
        private readonly IProductService _productService = productService;
		public IActionResult Index()
		{
			try
			{
				HomeVM vm = new()
				{
					GetServices = _serviceService.GetAll().Data,
					GetCategoriesIsFeatured = _categoryService.GetAllIsFeatured().Data,
					GetBrands = _brandService.GetAll().Data,
					GetProductListIsFeatured = _productService.GetProductListIsFeatured().Data,
					GetNewestProductsIsFeatuerd = _productService.GetNewestProductsIsFeatuerd().Data,
				};
                return View(vm);
            }
			catch (Exception ex)
			{
                return View(ex);
            }
		}
        public IActionResult AccessDenied()
        {
            return View(); 
        }

    }
}
