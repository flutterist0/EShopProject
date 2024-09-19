using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OperationClaimController(IOperationClaimService operationClaimService) : ControllerBase
	{
		private readonly IOperationClaimService _operationClaimService = operationClaimService;
		[HttpPost]
		public IActionResult AddOperationClaim(OperationClaim operationClaim)
		{
			var result = _operationClaimService.Add(operationClaim);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var result = _operationClaimService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("get{id:int:min(1)}")]
		public IActionResult Get(int id)
		{
			var result = _operationClaimService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var result = _operationClaimService?.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else return BadRequest(result);
		}
		[HttpPut]
		public IActionResult Update(OperationClaim operationClaim, int id)
		{
			var result = _operationClaimService.Update(operationClaim, id);
			if (result.Success)
			{
				return Ok(result);
			}
			else return BadRequest(result);
		}
	}
}
