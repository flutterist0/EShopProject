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
		//public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto)
		//{
		//	var result = _productService.Update(productUpdateDto);
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}else
		//		return BadRequest(result);
		//}
	}
}
