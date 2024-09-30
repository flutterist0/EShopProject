using Entities.Concrete;
using Entities.Dto.ProductDtos;

namespace EShopUI.Models
{
    public class HomeVM
    {
        public List<Service> GetServices { get; set; }
        public List<Category> GetCategoriesIsFeatured { get; set; }
        public List<Brand> GetBrands { get; set; }
        public List<ProductListDto> GetProductListIsFeatured { get; set; }
        public List<ProductListDto> GetNewestProductsIsFeatuerd { get; set; }
    }
}
