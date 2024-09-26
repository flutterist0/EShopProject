using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ProductDtos
{
	public class ProductToCategoryListDto:IDto	
	{
		public string Name { get; set; }
		public decimal? Price { get; set; }
		public bool IsDiscount { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string ImageUrl { get; set; }
	}
}
