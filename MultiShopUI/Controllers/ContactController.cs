using Business.Abstract;
using Entities.Concrete;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Controllers
{
    public class ContactController(IContactFormService contactFormService,IContactService contactService) : Controller
    {
        private IContactFormService _contactFormService = contactFormService;
        private IContactService _contactService = contactService;
        public IActionResult Contact()
        {
            try
            {
                ContactVM vm = new()
                {
                    GetContacts = _contactService.GetAll().Data,
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
    
        }

        public IActionResult ContactForm(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                var result = _contactFormService.Add(contactForm);
                if (result.Success)
                {
                    return RedirectToAction("Contact", "Contact");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View("xeta");
        }
    }
}
