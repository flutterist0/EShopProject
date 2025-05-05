using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserOperationClaimController(IUserOperationClaimService userOperationClaimService,IUserService userService) : ControllerBase
	{
		private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;
		private readonly IUserService _userService = userService;
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
		[HttpDelete("Delete")]
		public IActionResult Delete(int userId, int operationClaimId)
		{
			var result = _userOperationClaimService.Delete(userId, operationClaimId);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);	
		}

		[HttpGet("getUserOperationClaims")]
		public IActionResult GetUsersWithOperationClaim()
		{
			var result = _userService.GetUsersWithOperationClaim();
            if (result.Count>0)
            {
                return Ok(result);
            }
            else
                return BadRequest(result);

        }

        [HttpGet("getUserOperationClaimById {userId:int:min(1)}")]
        public IActionResult GetUsersWithOperationClaimById(int userId)
        {
            var result = _userOperationClaimService.GetUserOperationClaimsById(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
                return BadRequest(result);

        }


    }
}
