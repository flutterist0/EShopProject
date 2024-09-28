using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ShippingAddressGetAllDto:IDto
    {
        public int ShippingAddressId { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }
    }
}
