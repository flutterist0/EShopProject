using Entities.Concrete;

namespace EShopUI.Models
{
    public class HomeVM
    {
      public  List<Service> GetServices {  get; set; }
      public List<Category> GetCategoriesIsFeatured { get; set; }
        public List<Brand> GetBrands { get; set; }
    }
}
