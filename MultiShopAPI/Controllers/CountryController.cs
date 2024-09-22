using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController(ICountryService countryService) : ControllerBase
	{
		private readonly ICountryService _countryService = countryService;
		[HttpPost]
		public IActionResult Add(Country country)
		{
			var result = _countryService.Add(country);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getAllCountries")]
		public IActionResult GetAll()
		{
			var result = _countryService.GetAll();
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
			var result = _countryService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpPut]
		public IActionResult Update(Country country, int id)
		{
			var result = _countryService.Update(country, id);
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
			var result = _countryService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
	}
}
