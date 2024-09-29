using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouriteController(IFavouriteService favouriteService) : ControllerBase
	{
		private readonly IFavouriteService _favouriteService = favouriteService;
		[HttpPost]
		public IActionResult Add(int productId, int userId,int quantity)
		{
			var result = _favouriteService.AddFavourite(productId, userId, quantity);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }

		[HttpGet("getAll:{userId:int:min(1)}")]
		public IActionResult GetAll(int userId)
		{
			var result = _favouriteService.GetAllFavorites(userId);
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
			var result = _favouriteService.DeleteFavorite(productId, userId);
            if (result.Success)
            {
				return Ok(result);
            }else
			return BadRequest(result);
        }
	}
}
