using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactFormController(IContactFormService contactFormService) : ControllerBase
	{
		private readonly IContactFormService _contactFormService = contactFormService;
		[HttpPost]
		public IActionResult Add(ContactForm contactForm)
		{
			var result = _contactFormService.Add(contactForm);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }
	}
}
