using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShippingAddressController(IShippingAddressService shippingAddressService) : ControllerBase
	{
		private readonly IShippingAddressService _shippingAddressService = shippingAddressService;
		[HttpPost]
		public IActionResult Add(ShippingAddressAddDto shippingAddressAddDto)
		{
			var result = _shippingAddressService.Add(shippingAddressAddDto);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }
	}
}
