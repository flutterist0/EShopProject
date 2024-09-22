using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController(ICategoryService categoryService) : ControllerBase
	{
		private readonly ICategoryService _categoryService = categoryService;
		[HttpPost]
		public IActionResult Add(CategoryAddDto category)
		{
			var result = _categoryService.Add(category);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getAllCategories")]
		public IActionResult GetAll()
		{
			var result = _categoryService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("get{id:int:min(1)}")]
		public IActionResult Get(int id)
		{
			var result = _categoryService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpPut]
		public IActionResult Update(Category category, int id)
		{
			var result = _categoryService.Update(category, id);
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
			var result = _categoryService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
	}
}
