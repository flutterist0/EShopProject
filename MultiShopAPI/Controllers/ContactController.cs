using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController(IContactService contactService) : ControllerBase
	{
		private readonly IContactService _contactService = contactService;
		[HttpPost]
		public IActionResult Add(Contact contact)
		{
			var result = _contactService.Add(contact);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpGet("getAllContacts")]
		public IActionResult GetAll()
		{
			var result = _contactService.GetAll();
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
			var result = _contactService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpPut]
		public IActionResult Update(Contact contact, int id)
		{
			var result = _contactService.Update(contact, id);
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
			var result = _contactService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
	}
}
