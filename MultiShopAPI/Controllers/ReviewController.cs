using Business.Abstract;
using Entities.Dto.ReviewDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ReviewController(IReviewService reviewService) : ControllerBase
	{
		private readonly IReviewService _reviewService = reviewService;
		[HttpPost]
		public IActionResult Add(ReviewAddDto reviewAddDto)
		{
			var result = _reviewService.Add(reviewAddDto);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }

		[HttpGet("getAllByProductId:{productId:int:min(1)}")]
		public IActionResult GetAllByProductId(int productId)
		{
			var result = _reviewService.GetAllByProductId(productId);
			if (result.Success)
			{
				return Ok(result);
			}
			else return BadRequest(result);
		}
	}
}
