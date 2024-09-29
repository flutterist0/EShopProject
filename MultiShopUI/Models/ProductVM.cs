using Entities.Concrete;
using Entities.Dto;
using Entities.Dto.ProductDtos;
using Entities.Dto.ReviewDtos;

namespace EShopUI.Models
{
    public class ProductVM
    {
        public List<ProductListDto> GetProductList {get; set;}
        public ProductDetailDto ProductDetail { get; set; }
        public List<ReviewGetAllDto> GetReviewByProductId { get; set; }
        public List<CategoryWithProductsDto> GetAllCategoriesWithProducts { get; set; }
        public List<BrandWithProductsDto> GetAllBrandsWithProducts { get; set; }

        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
