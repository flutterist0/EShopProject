using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using Entities.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CartManager(ICartDal cartDal, IProductDal productDal,ICartItemDal cartItemDal, IProductImageDal productImageDal,IOrderDal orderDal) : ICartService
	{
		private readonly ICartDal _cartDal = cartDal;
		private readonly IProductDal _productDal = productDal;
		private readonly ICartItemDal _cartItemDal = cartItemDal;
		private readonly IProductImageDal _productImageDal = productImageDal;
		private readonly IOrderDal _orderDal = orderDal;	
		public IResult AddCart(int productId, int userId, int quantity)
		{
			var product = _productDal.Get(p => p.Id == productId);
			if (product == null)
			{
				return new ErrorResult("Məhsul tapılmadı");
			}

			var cart = _cartDal.GetCartByUserId(userId);
			if (cart == null)
			{
				cart = new Cart
				{
					UserId = userId,
					CreatedAt = DateTime.Now
				};
				_cartDal.Add(cart);

			}
			var cartItem = _cartItemDal.Get(ci => ci.ProductId == productId && ci.CartId == cart.Id);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                _cartItemDal.Update(cartItem);
                return new SuccessResult("Məhsul miqdari artirildi");
            }
            var newCart = new CartItem
			{
				CartId = cart.Id,
				ProductId = productId,
				Quantity = quantity,
				Price = product.IsDiscount ? product.DiscountPrice : product.Price,
				AddedAt = DateTime.Now
			};
			_cartItemDal.Add(newCart);
			return new SuccessResult("Məhsul səbətə əlavə olundu");
		}

		public IResult DeleteCart(int userId, int productId)
		{
			var cart = _cartDal.GetCartByUserId(userId);
			if (cart == null)
			{
				return new ErrorResult("Səbət tapılmadı");
			}

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null)
			{
				return new ErrorResult("Məhsul səbətdə tapılmadı");
			}
			cartItem.IsDelete = true;
			cart.CartItems.Remove(cartItem);
			_cartDal.Update(cart);

			return new SuccessResult("Məhsul səbətdən silindi");
		}

		public IDataResult<List<CartItemDto>> GetAllCarts(int userId)
		{
           var cart = _cartDal.GetCartByUserId(userId);
			if (cart == null)
			{
				return new ErrorDataResult<List<CartItemDto>>("Səbət tapılmadı");
			}

			var items = _cartItemDal.GetAll(ci => ci.CartId == cart.Id);

			var dtos = items
				.Select(ci =>
				{
					var product = _productDal.Get(p => p.Id == ci.ProductId);
					var productImages = _productImageDal.GetAll(pi => pi.ProductId == product.Id).ToList();
					var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl ?? "defaultimage-url";
                    return new CartItemDto
					{
						ProductName = product.Name,
						ProductImageUrl = firstImageUrl,
						Price = product.IsDiscount ? product.DiscountPrice : product.Price,
						Quantity = ci.Quantity
					};
				}
				
				).ToList();
			
			return new SuccessDataResult<List<CartItemDto>>(dtos);


        }

        public IDataResult<CheckoutDto> GetCheckoutDetails(int userId)
        {
            var cart = _cartDal.GetCartByUserId(userId);
            if (cart == null)
            {
                return new ErrorDataResult<CheckoutDto>("Səbət tapılmadı");
            }

            var items = _cartItemDal.GetAll(ci => ci.CartId == cart.Id);

            decimal subTotal = 0;
            decimal shippingCost = 0;

            var productDtos = items.Select(ci =>
            {
                var product = _productDal.Get(p => p.Id == ci.ProductId);

		
                decimal? price = product.IsDiscount ? product.DiscountPrice ?? product.Price : product.Price;
                decimal? totalPrice = price * ci.Quantity;

                // Ümumi məbləğə əlavə etmək
                subTotal += totalPrice??0;

                // Əgər məhsulda çatdırılma haqqı varsa, onu əlavə edirik
                if (product.IsDelivery)
                {
                    shippingCost += product.ShippingCost??0;
                }

                return new ProductCheckoutDto
                {
                    ProductName = product.Name,
                    Price = price,
                    Quantity = ci.Quantity,
                    TotalPrice = totalPrice
                };
            }).ToList();

            // GrandTotal = subTotal + shippingCost
            decimal grandTotal = subTotal + shippingCost;

            var checkoutDto = new CheckoutDto
            {
                Products = productDtos,
                SubTotal = subTotal,
                ShippingCost = shippingCost,
                GrandTotal = grandTotal
            };
            foreach (var item in items)
            {
                var order = new Order
                {
                    UserId = userId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
					OrderDate = DateTime.Now,
					
                    // Digər sifariş atributlarını əlavə edin
                };

                _orderDal.Add(order); // Sifarişlərdə istifadə olunan repository əlavə et

                // Stok yeniləmək
                var product = _productDal.Get(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity; // Stokdan alınan quantity-ni çıxırıq
                    _productDal.Update(product); // Stok yeniləmək üçün məhsulu yeniləyirik
                }
            }

            // Səbəti boşaltmağı unutmamalıyıq, əgər tələb olunursa
            _cartItemDal.DeleteRange(items); // Səbətdən bütün məhsulları silin

            return new SuccessDataResult<CheckoutDto>(checkoutDto);
        }
       

    }
}
