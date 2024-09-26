using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService,IHttpContextAccessor context) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
		[HttpPost]
		public IActionResult Add(int productId, int userId, int quantity)
		{
			var result = _cartService.AddCart(productId, userId, quantity);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
		[HttpGet("getAll:{userId:int:min(1)}")]
		public IActionResult GetAll(int userId)
		{
			
			var result = _cartService.GetAllCarts(userId);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpDelete]
		public IActionResult Delete(int productId, int userId)
		{
			var result = _cartService.DeleteCart(userId, productId);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

        [HttpGet("getCheckoutDetails:{userId:int:min(1)}")]
        public IActionResult GetCheckoutDetails(int userId)
        {

            var result = _cartService.GetCheckoutDetails(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
                return BadRequest(result);
        }
    }
}
