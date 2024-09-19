using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserOperationClaimController(IUserOperationClaimService userOperationClaimService) : ControllerBase
	{
		private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;
		[HttpPost("Add")]
		public IActionResult Add(int userId,int operationClaimId)
		{
			var result = _userOperationClaimService.Add(userId, operationClaimId);
            if (result.Success)
            {
				return Ok();
            }else
				return BadRequest(result);

        }


	}
}
