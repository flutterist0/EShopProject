using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CartManager(ICartDal cartDal, IProductDal productDal,ICartItemDal cartItemDal, IProductImageDal productImageDal) : ICartService
	{
		private readonly ICartDal _cartDal = cartDal;
		private readonly IProductDal _productDal = productDal;
		private readonly ICartItemDal _cartItemDal = cartItemDal;
		private readonly IProductImageDal _productImageDal = productImageDal;
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

			var dtos = items.GroupBy(c => c.ProductId)
				.Select(ci =>
				{
					var product = _productDal.Get(p => p.Id == ci.Key);
					var productImages = _productImageDal.GetAll(pi => pi.ProductId == product.Id).ToList();
					var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl ?? "defaultimage-url"; ;
					return new CartItemDto
					{
						ProductName = product.Name,
						ProductImageUrl = firstImageUrl,
						Price = product.IsDiscount ? product.DiscountPrice : product.Price,
						Quantity = ci.Sum(c => c.Quantity)
					};
				}
				
				).ToList();
			
			return new SuccessDataResult<List<CartItemDto>>(dtos);

		}
	}
}
