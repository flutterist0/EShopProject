using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfFavouriteDal:BaseRepository<Favourite,AppDbContext>,IFavouriteDal
	{
        public EfFavouriteDal(AppDbContext context) : base(context)
		{
            
        }

		public List<Favourite> GetAllFavoritesWithProductAndImages(int userId)
		{
			AppDbContext appDbContext = new AppDbContext();
			return appDbContext.Favourites
	   .Where(f => f.UserId == userId&&f.IsDelete==false)
	   .Include(f => f.Product)
	   .ThenInclude(p => p.ProductImages)
	   .ToList();
		}
	}
}
