using Business.Abstract;
using Entities.Dto.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]

    public class ProductsController(IProductService productService,IProductImageService productImageService,ICategoryService categoryService) : Controller
	{
		private readonly IProductService _productService = productService;
		private readonly IProductImageService _productImageService = productImageService;
		private readonly ICategoryService _categoryService = categoryService;
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddProduct()
		{
            ViewBag.Categories = new SelectList(_categoryService.GetAll().Data, "Id", "Name");
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
                return View("Error:");
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

        [HttpGet("editProduct/{id}")]
        public IActionResult EditProduct(int id)
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
                Quantity = product.Data.Quantity,
                IsFeatured = product.Data.IsFeatured,
                CategoryId = product.Data.CategoryId,
                ExistingImages = _productImageService.GetProductImages(product.Data.Id), 
            };
            ViewBag.Categories = new SelectList(_categoryService.GetAll().Data, "Id", "Name");
            return View(productUpdateDto);
        }

        [HttpPost("editProduct")]
        public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto, List<int> DeleteImages)
        {
            if (!ModelState.IsValid)
            {
                return View(productUpdateDto);
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
