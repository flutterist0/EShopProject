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
	public class CartManager(ICartDal cartDal, IProductDal productDal,ICartItemDal cartItemDal) : ICartService
	{
		private readonly ICartDal _cartDal = cartDal;
		private readonly IProductDal _productDal = productDal;
		private readonly ICartItemDal _cartItemDal = cartItemDal;
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
					CreatedAt = DateTime.Now,
					CartItems = new List<CartItem>()
				};
				_cartDal.Add(cart);

			}

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem != null)
			{
				cartItem.Quantity += quantity;
				_cartItemDal.Update(cartItem);

			}
			else
			{
				cartItem = new CartItem
				{
					CartId = cart.Id,
					ProductId = productId,
					Quantity = quantity,
					Price = product.IsDiscount ? product.DiscountPrice : product.Price,
					AddedAt = DateTime.Now
				};
				//cart.CartItems.Add(cartItem);
				_cartItemDal.Add(cartItem);
			}
			_cartDal.Update(cart);
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

			var cartItemsDto = cart.CartItems.Select(cartItem => new CartItemDto
			{
				ProductName = cartItem.Product.Name ?? "Məhsul yoxdur",
				ProductImageUrl = cartItem.Product.ProductImages.FirstOrDefault().ImageUrl ?? "No Image",
				Price = cartItem.Price,
				Quantity = cartItem.Quantity
			}).ToList();
			return new SuccessDataResult<List<CartItemDto>>(cartItemsDto);

		}
	}
}
