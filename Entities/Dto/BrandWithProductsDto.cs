using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class BrandWithProductsDto:IDto
	{
		public int BrandId { get; set; }
		public string Name { get; set; }
        public bool IsDelete { get; set; }
        public List<Product> Products { get; set; }
	}
}
