using Core.Entities.Abstract;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class ShippingAddressAddDto:IDto
	{

        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int ZipCode { get; set; }
        public string Street { get; set; }
		public int CountryId { get; set; } 
	}
}
