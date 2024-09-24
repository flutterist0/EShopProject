using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto.ProductDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfProductDal:BaseRepository<Product,AppDbContext>,IProductDal
	{
        public EfProductDal(AppDbContext context) : base(context)
		{
            
        }

		public List<ProductListDto> GetAllProducts()
		{
			AppDbContext appDbContext = new();
			var result = from p in appDbContext.Products
						 where p.IsDelete == false
						 select new ProductListDto
						 {
							 ProductId = p.Id,
							 Name = p.Name,
							 Price = p.Price,
							 DiscountPrice = p.DiscountPrice,
							 IsDiscount = p.IsDiscount,
							 IsFeatured = p.IsFeatured,
							 ImageUrl = appDbContext.ProductImages
						 .Where(pi => pi.ProductId == p.Id)
						 .Select(pi => pi.ImageUrl)
						 .FirstOrDefault(),
						 };
			return result.ToList();
		}

		public Product GetById(int productId)
		{
			AppDbContext appDbContext = new();
			return appDbContext.Products
				.Include(p => p.ProductImages) 
				.FirstOrDefault(p => p.Id == productId);
		}
	}
}
