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
		public string Name { get; set; }  // Məhsulun adı
		public decimal Price { get; set; }  // Məhsulun qiyməti
		public decimal? DiscountPrice { get; set; }  // İndirimli qiymət (əgər varsa)
		public string Description { get; set; }  // Məhsul haqqında təsvir
		public string Spesification { get; set; }  // Məhsulun spesifikasiyaları
		public string CategoryName { get; set; }  // Məhsulun aid olduğu kateqoriyanın adı
		public List<string> Images { get; set; }  // Məhsulun şəkilləri siyahısı
	}
}
