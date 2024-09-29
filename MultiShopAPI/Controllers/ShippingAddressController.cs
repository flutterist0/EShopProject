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
		public IActionResult Add(ShippingAddressAddDto shippingAddressAddDto,int userId)
		{
			var result = _shippingAddressService.Add(shippingAddressAddDto,userId);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }

		[HttpGet("getByUserIdShippingAddresses: {userId:int:min(1)}")]
		public IActionResult GetByUserIdShippingAddresses(int userId)
		{
			var result = _shippingAddressService.GetByUserIdShippingAddresses(userId);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);
		}

		[HttpDelete("delete: {userId:int:min(1)}")]
		public IActionResult Delete(int userId)
		{
			var result = _shippingAddressService.Delete(userId);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);
		}
    }
}
