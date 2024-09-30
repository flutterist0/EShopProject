using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IBaseRepository<Product>
	{
		List<ProductListDto> GetAllProducts();
        List<ProductListDto> GetAllProductsIsFeatured();
        Product GetById(int productId);
	}
}
