using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfBrandDal:BaseRepository<Brand,AppDbContext>,IBrandDal
    {
        public EfBrandDal(AppDbContext context) : base(context)
		{
            
        }

		public List<BrandWithProductsDto> GetAllBrandsWithProducts()
		{
			var context = new AppDbContext();
			var brandsWithProducts = context.Brands
	 .Select(b => new BrandWithProductsDto
	 {
		 BrandId = b.Id,
		 Name = b.Name,
		 Products = context.Products
							 .Where(p => p.BrandId == b.Id)
							 .ToList()
	 })
	 .ToList();
			return brandsWithProducts;
		}
	}
}
