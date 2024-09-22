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
		public IResult AddFavourite(int productId, int userId, int quantity)
		{
			var existingFavorite = _favouriteDal.Get(f => f.ProductId == productId && f.UserId == userId);

			if (existingFavorite != null)
			{
				existingFavorite.Quantity += quantity;
				_favouriteDal.Update(existingFavorite);
				return new SuccessResult("Product quantity updated in favorites");
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
			favorite.IsDelete = true;	
			_favouriteDal.Delete(favorite);
			return new SuccessResult("Product favoritlerden silindi");
		}

		public IDataResult<List<FavouriteDto>> GetAllFavorites(int userId)
		{
			var favorites = _favouriteDal.GetAllFavoritesWithProductAndImages(userId);

			if (favorites == null || !favorites.Any())
			{
				return new ErrorDataResult<List<FavouriteDto>>("Favourite product yoxdu");
			}
			var favoriteDtos = favorites.Select(f => new FavouriteDto
			{
				ProductName = f.Product.Name,
				Price = f.Product.Price,
				ImageUrl = f.Product.ProductImages.FirstOrDefault()?.ImageUrl ?? "",
				Quantity = f.Quantity 
			}).ToList();
			return new SuccessDataResult<List<FavouriteDto>>(favoriteDtos);
		}


	}
}
