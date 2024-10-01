using Business.Abstract;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        private readonly ICartService _cartService = cartService;
        public IActionResult Cart()
        {
            int userId = int.Parse(Request.Cookies["userId"]??"0");
            if (userId == null || userId == 0) 
            {
                return RedirectToAction("Login","Auth");
            }
            var vm = new CartVM 
            {
                CartItems = _cartService.GetAllCarts(userId).Data 
            };
            if (vm.CartItems!=null)
            {
                 ViewData["UserId"] = userId;
                return View(vm);
            }
  
            ViewData["UserId"] = userId;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            int userId = int.Parse(Request.Cookies["userId"]);
            var result = _cartService.AddCart(productId, userId, quantity);

            if (result.Success)
            {
                return RedirectToAction("Cart");
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId,int userId)
        {
      
            var result = _cartService.DeleteCart(userId, productId);

            if (result.Success)
            {
                return RedirectToAction("Cart");
            }
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Cart");
        }

    }
}
