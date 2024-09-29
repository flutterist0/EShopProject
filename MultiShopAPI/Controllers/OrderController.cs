using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost]
        public IActionResult AddOrder(int userId,int shippingAddressId)
        {
            var result = _orderService.AddOrder(userId, shippingAddressId);
            if (result.Success)
            {
                return Ok(result);
            }else
                return BadRequest(result);
        }


        [HttpGet("getOrdersByUserId:{userId:int:min(1)}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var result = _orderService.GetOrdersByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }else
                return BadRequest(result);
        }
    }
}
