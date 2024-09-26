using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using Entities.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService
	{
		IResult Add(ProductImageAddDto productImageAddDto);
        List<ProductImage> GetProductImages(int productId);
    }
}
