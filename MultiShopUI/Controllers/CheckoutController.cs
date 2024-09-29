using Business.Abstract;
using DataAccess.Abstract;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopUI.Controllers
{
    public class CheckoutController(ICartService cartService,IOrderService orderService,IShippingAddressService shippingAddressService,ICountryService countryService) : Controller
    {
        private readonly IShippingAddressService _shippingAddressService = shippingAddressService;
        private readonly ICartService _cartService = cartService;
        private readonly ICountryService _countryService = countryService;  
        private readonly IOrderService _orderService = orderService;
        public IActionResult Checkout()
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            try
            {
                CheckoutVM vm = new()
                {
                    GetCheckout = _cartService.GetCheckoutDetails(userId).Data,
                    GetShippingAddress = _shippingAddressService.GetByUserIdShippingAddresses(userId).Data,
                };
                ViewBag.Countries = new SelectList(_countryService.GetAll().Data, "Id", "Name");
                return View(vm);
            }
            catch(Exception ex) 
            {
                return View(ex);
            }

        }

        public IActionResult AddOrder()
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            var getShippingAddress = _shippingAddressService.GetByUserIdShippingAddresses(userId);
            if (ModelState.IsValid)
            {
                var result = _orderService.AddOrder(userId, getShippingAddress.Data.ShippingAddressId);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View("Xeta");


        }
    }
}
