using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class FavouriteManager(IFavouriteDal favouriteDal) : IFavouriteService
	{
		private readonly IFavouriteDal _favouriteDal = favouriteDal;
		public IResult AddFavourite(int productId, int userId,int quantity)
		{
			var existingFavorite = _favouriteDal.Get(f => f.ProductId == productId && f.UserId == userId);

			if (existingFavorite != null)
			{
				return new SuccessResult("Product is in favorites");
			}
			var favorite = new Favourite
			{
				ProductId = productId,
				UserId = userId,
				Quantity = quantity
			};
			_favouriteDal.Add(favorite);

			return new SuccessResult("Product added to favorites");
		}


        public IResult DeleteFavorite(int productId, int userId)
		{
			var favorite = _favouriteDal.Get(f => f.ProductId == productId && f.UserId == userId);

			if (favorite == null)
			{
				return new ErrorResult("Favorite yoxdu");
			}

            _favouriteDal.DeleteX(favorite);
			return new SuccessResult("Product favoritlerden silindi");
		}

		public IDataResult<List<FavouriteGetAllDto>> GetAllFavorites(int userId)
		{
			var favorites = _favouriteDal.GetAllFavoritesWithProductAndImages(userId);

			if (favorites == null || !favorites.Any())
			{
				return new ErrorDataResult<List<FavouriteGetAllDto>>("Favourite product yoxdu");
			}
			var favoriteDtos = favorites.Select(f => new FavouriteGetAllDto
			{
				ProductId = f.ProductId,
				ProductName = f.Product.Name,
				Price = f.Product.Price,
				ImageUrl = f.Product.ProductImages.FirstOrDefault()?.ImageUrl ?? "",
				Quantity = f.Quantity,
				DiscountPrice = f.Product.DiscountPrice,
				IsDiscount = f.Product.IsDiscount,
			}).ToList();
			return new SuccessDataResult<List<FavouriteGetAllDto>>(favoriteDtos);
		}


	}
}
