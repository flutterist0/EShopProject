using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ProductDtos
{
	public class ProductDetailDto:IDto
	{
		public string Name { get; set; }  
		public decimal? Price { get; set; } 
		public decimal? DiscountPrice { get; set; } 
		public string Description { get; set; } 
		public string Spesification { get; set; } 
		public string CategoryName { get; set; }
		public List<string> Images { get; set; }  
	}
}
