using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class CategoryWithProductsDto:IDto
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public List<Product> Products { get; set; }
	}

}
