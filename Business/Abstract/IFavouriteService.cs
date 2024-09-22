using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IFavouriteService
	{
		IResult AddFavourite(int productId, int userId, int quantity);
		IResult DeleteFavorite(int productId, int userId);
		IDataResult<List<FavouriteDto>> GetAllFavorites(int userId);
	}
}
