using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ProductDtos
{
	public class ProductImageUpdateDto:IDto
	{
		public IFormFile Image { get; set; }
		public int ProductId { get; set; }
		public bool IsFeatuerd { get; set; }
	}
}
