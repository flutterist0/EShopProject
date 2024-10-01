using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Entities.Dto.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]

    public class ProductsController(IProductService productService,IProductImageService productImageService,ICategoryService categoryService,IBrandService brandService) : Controller
	{
		private readonly IProductService _productService = productService;
		private readonly IProductImageService _productImageService = productImageService;
		private readonly ICategoryService _categoryService = categoryService;
        private readonly IBrandService _brandService = brandService;
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddProduct()
		{
            ViewBag.Categories = new SelectList(_categoryService.GetAll().Data, "Id", "Name");
            ViewBag.Brands = new SelectList(_brandService.GetAll().Data, "Id", "Name");
            return View();
		}

		[HttpPost]
		public IActionResult AddProduct(ProductAddDto productAddDto)
		{
			if (ModelState.IsValid)
			{
				var result = _productService.Add(productAddDto);

				if (result.Success)
				{
                    return RedirectToAction("GetAllProducts", "Products"); 
                }
				else
				{
					ModelState.AddModelError("", result.Message);
				}
			}
            ViewBag.Categories = new SelectList(_categoryService.GetAll().Data, "Id", "Name");
            ViewBag.Brands = new SelectList(_brandService.GetAll().Data, "Id", "Name");
            return View(productAddDto);
		}

        public IActionResult GetAllProducts()
		{
			try
			{
				var products = _productService.GetProductList();
				return View(products.Data);
			}catch(Exception ex)
			{
                 return RedirectToAction("AccessDenied", "Dashboard");
            }
		}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.Get(id).Data;
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _productService.Delete(id);
                if (result.Success)
                {
                    return RedirectToAction("GetAllProducts", "Products");
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

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var product = _productService.Get(id); 

            if (product == null)
            {
                return NotFound(); 
            }
            var productUpdateDto = new ProductUpdateDto
            {
                ProductId = product.Data.Id,
                Name = product.Data.Name,
                Description = product.Data.Description,
                Spesification = product.Data.Spesification,
                Price = product.Data.Price,
                IsDiscount = product.Data.IsDiscount,
                DiscountPrice = product.Data.IsDiscount ? product.Data.DiscountPrice : 0,
                Stock = product.Data.Stock,
                BrandId = product.Data.BrandId, 
                IsFeatured = product.Data.IsFeatured,
                CategoryId = product.Data.CategoryId,
                ShippingCost = product.Data.ShippingCost,
                IsDelivery = product.Data.IsDelivery,
                ExistingImages = _productImageService.GetProductImages(product.Data.Id), 
            };
            ViewBag.Categories = new SelectList(_categoryService.GetAll().Data, "Id", "Name");
            ViewBag.Brands = new SelectList(_brandService.GetAll().Data, "Id", "Name");
            return View(productUpdateDto);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto, List<int> DeleteImages)
        {
            if (!ModelState.IsValid)
            {
                return View(productUpdateDto);
            }
            if (productUpdateDto.ProductImages==null)
            {
                productUpdateDto.ProductImages = new();
            }
            var result = _productService.Update(productUpdateDto, DeleteImages);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(productUpdateDto);
            }

            return RedirectToAction("GetAllProducts","Products");
        }



    }
}
