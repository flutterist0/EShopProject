using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentMethodController(IPaymentMethodService paymentMethodService) : ControllerBase
	{
		private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;
		[HttpPost]
		public IActionResult Add(PaymentMethod paymentMethod)
		{
			var result = _paymentMethodService.Add(paymentMethod);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getAllPaymentMethods")]
		public IActionResult GetAll()
		{
			var result = _paymentMethodService.GetAll();
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
			var result = _paymentMethodService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpPut]
		public IActionResult Update(PaymentMethod paymentMethod, int id)
		{
			var result = _paymentMethodService.Update(paymentMethod, id);
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
			var result = _paymentMethodService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
	}
}
