using Business.Abstract;
using Entities.Dto.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
		[HttpPost("add")]
		public IActionResult Add(ProductAddDto productAddDto)
		{
			var result = _productService.Add(productAddDto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
		[HttpGet("getProductList")]
		public IActionResult GetProductList()
		{
			var result = _productService.GetProductList();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getDetailProduct:{id:int:min(1)}")]
		public IActionResult GetDetailProduct(int id)
		{
			var result = _productService.GetProductDetails(id);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);
		}

		[HttpGet("getProductsByCategoryId:{categoryId:int:min(1)}")]
		public IActionResult GetProductsByCategoryId(int categoryId)
		{
			var result = _productService.GetProductsByCategory(categoryId);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }

		[HttpGet("getNewestProducts")]
		public IActionResult GetNewestProducts()
		{
			var result = _productService.GetNewestProducts();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getProductsSortedByPriceAscending")]
		public IActionResult GetProductsSortedByPriceAscending()
		{
			var result = _productService.GetProductsSortedByPriceAscending();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getProductsSortedByPriceDescending")]
		public IActionResult GetProductsSortedByPriceDescending()
		{
			var result = _productService.GetProductsSortedByPriceDescending();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getProductsByCategoryWithPagination")]
		public IActionResult GetProductsByCategoryWithPagination(int categoryId, int pageNumber, int pageSize)
		{
			var result = _productService.GetProductsByCategoryWithPagination(categoryId, pageNumber, pageSize);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpDelete("delete:{id:int:min(1)}")]
		public IActionResult Delete(int id)
		{
			var result = _productService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);
		}
		//[HttpPut]
		//public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto, List<int> deleteImageIds)
		//{
		//	var result = _productService.Update(productUpdateDto, deleteImageIds);
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	else
		//		return BadRequest(result);
		//}
	}
}
