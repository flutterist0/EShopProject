using Entities.Concrete;
using Entities.Dto;

namespace EShopUI.Models
{
    public class AccountViewModel
    {
        public List<OrderDto> Orders { get; set; }
        public ShippingAddressGetAllDto ShippingAddresses { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public UserGetDto User { get; set; }
        public UserUpdateDto UpdtUsr { get; set; }
        public ChangePasswordDto ChangePasswordDto { get; set; }
    }
}
