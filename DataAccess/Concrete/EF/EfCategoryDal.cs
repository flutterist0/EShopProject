using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfCategoryDal:BaseRepository<Category,AppDbContext>,ICategoryDal
    {
        public EfCategoryDal(AppDbContext context) : base(context)
		{
            
        }

		public List<CategoryWithProductsDto> GetAllCategoriesWithProducts()
		{
			var context = new AppDbContext();
			var categoriesWithProducts =  context.Categories
	 .Select(c => new CategoryWithProductsDto
	 {
		 CategoryId = c.Id,
		 Name = c.Name,
		 Products = context.Products
							 .Where(p => p.CategoryId == c.Id)
							 .ToList() 
	 })
	 .ToList();
			return categoriesWithProducts;
		}
	}
}
