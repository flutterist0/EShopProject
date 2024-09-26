using Core.Entities.Abstract;
using Entities.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CheckoutDto:IDto
    {
        public List<ProductCheckoutDto> Products { get; set; } = new List<ProductCheckoutDto>();
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
