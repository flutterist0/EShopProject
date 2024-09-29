using Entities.Dto;

namespace EShopUI.Models
{
    public class CheckoutVM
    {
        public CheckoutDto GetCheckout { get; set; }
        public ShippingAddressGetAllDto GetShippingAddress { get; set; }
    }
}
