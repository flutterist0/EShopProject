using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ContactsController(IContactService contactService,IUserOperationClaimService userOperationClaimService) : Controller
    {
        private readonly IContactService _contactService = contactService;
        private readonly IUserOperationClaimService _userOperationClaimService = userOperationClaimService;

        public IActionResult ContactsList()
        {
            try
            {
                int userId = int.Parse(Request.Cookies["userId"] ?? "0");

                var admin = _userOperationClaimService.GetUserOperationClaimsById(userId);
                if (admin.Data.OperationClaimName == "Admin")
                {
                    var result = _contactService.GetAll().Data;
                    return View(result);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Dashboard");
                }
          
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Dashboard");
            }


        }

        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var result = _contactService.Add(contact);

                if (result.Success)
                {
                    return RedirectToAction("ContactsList", "Contacts");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(contact);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.Get(id).Data;
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _contactService.Delete(id);
                if (result.Success)
                {
                    return RedirectToAction("ContactsList", "Contacts");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return RedirectToAction("ContactsList", "Contacts");
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

    }
}
