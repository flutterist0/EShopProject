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
		IResult Update(ProductUpdateDto productUpdateDto,List<int> deleteImageIds);
		IResult Delete(int id);
		IDataResult<List<Product>> GetAll();
		IDataResult<List<ProductListDto>> GetProductsByCategory(int categoryId);
        IDataResult<List<ProductListDto>> GetProductsByBrand(int brandId);
        IDataResult<List<ProductListDto>> GetProductList();
		IDataResult<List<ProductListDto>> GetNewestProducts();
		IDataResult<Product> Get(int id);
		IDataResult<ProductDetailDto> GetProductDetails(int productId);
		IDataResult<List<ProductListDto>> GetProductsSortedByPriceAscending();
		IDataResult<List<ProductListDto>> GetProductsSortedByPriceDescending();
		IDataResult<PagedResult<ProductListDto>> GetProductsByCategoryWithPagination(int categoryId, int pageNumber, int pageSize);
	}
}
