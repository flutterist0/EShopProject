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
	public class EfProductImageDal:BaseRepository<ProductImage,AppDbContext>,IProductImageDal
	{
        public EfProductImageDal(AppDbContext context) : base(context)
		{
            
        }

        public void DeleteImage(Product product, List<int> deleteIds)
        {
            using (var context = new AppDbContext())
            {
                var imagesToDelete = context.ProductImages
                                             .Where(pi => deleteIds.Contains(pi.Id) && pi.ProductId == product.Id)
                                             .ToList();
                context.ProductImages.RemoveRange(imagesToDelete);
                context.SaveChanges();
            }
        }
    }
}
