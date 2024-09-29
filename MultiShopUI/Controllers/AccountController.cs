using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dto;
using EShopUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static NuGet.Packaging.PackagingConstants;

namespace EShopUI.Controllers
{
    public class AccountController(IShippingAddressService shippingAddressService,IOrderService orderService,IPaymentMethodService paymentMethodService,IUserService userService,ICountryService countryService) : Controller
    {
        private readonly IShippingAddressService _shippingAddressService = shippingAddressService;
        private readonly IOrderService _orderService = orderService;
        private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;
        private readonly IUserService _userService = userService;
        private readonly ICountryService _countryService = countryService;
        public IActionResult Account()
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            var orderResult = _orderService.GetOrdersByUserId(userId);
            var shippingAddressResult = _shippingAddressService.GetByUserIdShippingAddresses(userId);
            var paymentMethodsResult = _paymentMethodService.GetAll();
            var userGetResult = _userService.GetById(userId);
            AccountViewModel vm = new()
            {
                PaymentMethods = paymentMethodsResult.Success ?paymentMethodsResult.Data : new List<PaymentMethod>(),
                Orders = orderResult.Success ? orderResult.Data : new List<OrderDto>(),
                ShippingAddresses = shippingAddressResult.Success ? shippingAddressResult.Data : new ShippingAddressGetAllDto(),
                User = userGetResult.Success? userGetResult.Data : new UserGetDto()

            };
            return View(vm);
        }
        public IActionResult UpdateUser(AccountViewModel accountViewModel)
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            //var result = _userService.Update(userUpdateDto, userId);
            var result = _userService.Update(new UserUpdateDto
            {
                UserId = userId,
                FirstName = accountViewModel.User.FirstName,
                LastName = accountViewModel.User.LastName,
                Email = accountViewModel.User.Email,
                PhoneNumber = accountViewModel.User.PhoneNumber
            }, userId);
            if (result.Success)
            {
                ViewBag.Message = result.Message;
            }
            else
            {
                ViewBag.Error = result.Message; 
            }

            var updatedUser = _userService.GetById(userId);
            var orderResult = _orderService.GetOrdersByUserId(userId);
            var shippingAddressResult = _shippingAddressService.GetByUserIdShippingAddresses(userId);
            var paymentMethodsResult = _paymentMethodService.GetAll();
            accountViewModel.User = updatedUser.Data;
            accountViewModel.Orders = orderResult.Data;
            accountViewModel.ShippingAddresses = shippingAddressResult.Data;
            accountViewModel.PaymentMethods = paymentMethodsResult.Data;
            return View("Account", accountViewModel);

        }
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            var result = _userService.ChangePassword(changePasswordDto, userId);
            if (result.Success)
            {
                ViewBag.Message = result.Message;
                return RedirectToAction("Account");
            }else
                ViewBag.Error = result.Message;
  return View(changePasswordDto);
        }
     

        public IActionResult DeleteShippingAddress()
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            try
            {
                var result = _shippingAddressService.Delete(userId);
                if (result.Success)
                {
                    return RedirectToAction("Account");
                }
                else
                {
                    return RedirectToAction("Account");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Account");
            }
        }
        [HttpGet]
        public IActionResult AddShippingAddress()
        {
            ViewBag.Countries = new SelectList(_countryService.GetAll().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddShippingAddress(ShippingAddressAddDto model)
        {
            var userId = int.Parse(Request.Cookies["userId"]);
            if (ModelState.IsValid)
            {
                var result = _shippingAddressService.Add(model,userId);
                if (result.Success)
                {
                    return RedirectToAction("Account");
                }

                ModelState.AddModelError("", result.Message);
            }
            ViewBag.Countries = new SelectList(_countryService.GetAll().Data, "Id", "Name");
            return View(model);
        }


    }
}
