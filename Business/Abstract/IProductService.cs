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
    public interface IProductService
    {
        IResult Add(ProductAddDto productAddDto);
		IResult Update(ProductUpdateDto productUpdateDto);
		IResult Delete(int id);
		IDataResult<List<Product>> GetAll();
		IDataResult<List<ProductListDto>> GetProductList();
		IDataResult<Product> Get(int id);
		IDataResult<ProductDetailDto> GetProductDetails(int productId);
	}
}
