using Entities.Dto;
using System.Collections.Generic;

namespace EShopUI.Models
{
    public class CartVM
    {
        public List<CartItemDto> CartItems { get; set; }
    }
}
