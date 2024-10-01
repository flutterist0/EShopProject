using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dto.ReviewDtos;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Drawing.Printing;

namespace EShopUI.Controllers
{
    public class ProductController(IProductService productService,IReviewService reviewService,ICategoryService categoryService,IBrandService brandService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IReviewService _reviewService = reviewService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IBrandService _brandService = brandService;
        public IActionResult Product(int pageNumber=1, int pageSize=3)
        {
            int userId = int.Parse(Request.Cookies["userId"] ?? "0");
            var result = _productService.GetProductsWithPagination(pageNumber, pageSize);

            if (result.Success)
            {
                ProductVM vm = new()
                {
                    GetProductList = result.Data.Items,
                    TotalCount = result.Data.TotalCount,
                    PageNumber = result.Data.PageNumber,
                    PageSize = result.Data.PageSize,
                    TotalPages = result.Data.TotalPages,
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                ViewData["UserId"] = userId;
                return View("Product", vm);
            }
            ViewData["UserId"] = userId;
            return View(result.Message);
            
        }
        public IActionResult ProductDetails(int id)
        {
            var reviews = _reviewService.GetAllByProductId(id).Data ?? new List<ReviewGetAllDto>();
            ProductVM vm = new()
            {
                ProductDetail = _productService.GetProductDetails(id).Data,
                GetReviewByProductId = reviews,
                GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
            };
            if (vm.ProductDetail!=null||vm.GetAllCategoriesWithProducts != null|| vm.GetReviewByProductId != null)
            {
                return View(vm);
            }else
                return View(vm);
        }
        [HttpPost]
        public IActionResult AddReview(ReviewAddDto reviewAddDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ProductDetails", new { id = reviewAddDto.ProductId });
            }

            var result = _reviewService.Add(reviewAddDto);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction("ProductDetails", new { id = reviewAddDto.ProductId });
        }


        public IActionResult GetReviews(int productId)
        {
            ProductVM vm = new()
            {
                GetReviewByProductId = _reviewService.GetAllByProductId(productId).Data,
            };
            if (vm.GetReviewByProductId != null)
            {
                return View(vm);
            }
            else
                return View(vm);
        }

        public IActionResult GetCategories()
        {
            ProductVM vm = new()
            {
				GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
			};
            if (vm.GetAllCategoriesWithProducts != null)
            {
                return View(vm);
            }else
                return NotFound();
        }
        public IActionResult GetBrands()
		{
			ProductVM vm = new()
			{
				GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
			};
			if (vm.GetAllBrandsWithProducts != null)
			{
				return View(vm);
			}
			else
				return NotFound();
		}
        public IActionResult GetNewestProducts()
        {
            try
            {
                var newestProducts = _productService.GetNewestProducts().Data;
                ProductVM vm = new()
                {
                    GetProductList = newestProducts, 
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                return View("Product", vm); 
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult GetProductsSortedByPriceAscending()
        {
            try
            {
                var productPriceAscending = _productService.GetProductsSortedByPriceAscending().Data;
                ProductVM vm = new()
                {
                    GetProductList = productPriceAscending,
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                return View("Product", vm);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
           
        }

        public IActionResult GetProductsSortedByPriceDescending()
        {
            try
            {
                var productPriceDescending = _productService.GetProductsSortedByPriceDescending().Data;
                ProductVM vm = new()
                {
                    GetProductList = productPriceDescending,
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                return View("Product", vm);
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var result = _productService.GetProductsByCategory(categoryId);
            if (result.Success)
            {
                ProductVM vm = new()
                {
                    GetProductList = result.Data,
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                return View("Product", vm);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Category(int id)
        {
            return RedirectToAction("GetProductsByCategory", new { categoryId = id });
        }

        public IActionResult GetProductsByBrand(int brandId)
        {
            var result = _productService.GetProductsByBrand(brandId);
            if (result.Success)
            {
                ProductVM vm = new()
                {
                    GetProductList = result.Data,
                    GetAllCategoriesWithProducts = _categoryService.GetAllCategoriesWithProducts().Data,
                    GetAllBrandsWithProducts = _brandService.GetAllBrandsWithProducts().Data,
                };
                return View("Product", vm);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Brand(int id)
        {
            return RedirectToAction("GetProductsByBrand", new { brandId = id });
        }
       
    }
}