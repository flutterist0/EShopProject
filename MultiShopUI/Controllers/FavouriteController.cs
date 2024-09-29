using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Concrete;
using Entities.Concrete;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Controllers
{
    public class FavouriteController(IFavouriteService favouriteService) : Controller
    {
        private readonly IFavouriteService _favouriteService = favouriteService;
        public IActionResult Favourite()
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            //var result = _favouriteService.GetAllFavorites(userId);

         
                var vm = new FavouriteVM
                {
                    FavouriteList = _favouriteService.GetAllFavorites(userId).Data
                };
            if (vm.FavouriteList != null)
            {
                ViewData["UserId"] = userId;
                return View(vm);
            }
            ViewData["UserId"] = userId;
            return View(vm);
        }

        [HttpPost]
        public IActionResult DeleteFavorite(int productId, int userId)
        {
            var result = _favouriteService.DeleteFavorite(productId, userId);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Favourite");
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Favourite");
        }

        [HttpPost]
        public IActionResult AddToFavorites(int productId, int quantity)
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            
            var result = _favouriteService.AddFavourite(productId, userId,quantity);

            if (result.Success)
            {
                return RedirectToAction("Favourite");
            }

            return RedirectToAction("ProductDetails", new { id = productId });
        }
    }
}
