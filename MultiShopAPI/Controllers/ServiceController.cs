using Business.Abstract;
using Entities.Concrete;
using Entities.Dto.ServiceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ServiceController(IServiceService serviceService) : ControllerBase
	{
		private readonly IServiceService _serviceService = serviceService;
		[HttpPost]
		public IActionResult Add(ServiceAddDto serviceAddDto)
		{
			var result = _serviceService.Add(serviceAddDto);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getAllServices")]
		public IActionResult GetAll()
		{
			var result = _serviceService.GetAll();
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
			var result = _serviceService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		//[HttpPut]
		//public IActionResult Update(ServiceUpdateDto service, int id)
		//{
		//	var result = _serviceService.Update(service, id);
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	else
		//		return BadRequest(result);
		//}

		[HttpDelete("delete:{id:int:min(1)}")]
		public IActionResult Delete(int id)
		{
			var result = _serviceService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
	}
}
