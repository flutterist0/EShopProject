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
	public class EfCartDal:BaseRepository<Cart,AppDbContext>,ICartDal
	{
        public EfCartDal(AppDbContext context) : base(context)
		{
            
        }

		public Cart GetCartByUserId(int userId)
		{
			AppDbContext appDbContext = new();
			return appDbContext.Carts
					  .Include(c => c.CartItems)
				   .FirstOrDefault(c => c.UserId == userId);
		}

		//public List<Cart> GetAllCartsWithProductAndImages(int userId)
		//{
		//	AppDbContext appDbContext = new AppDbContext();
		//	return appDbContext.Carts
		//  .Where(f => f.UserId == userId && f.IsDelete == false)
		//  .Include(f => f.Product)
		//  .ThenInclude(p => p.ProductImages)
		//  .ToList();
		//}
	}
}
