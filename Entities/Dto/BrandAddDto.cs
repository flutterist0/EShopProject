using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
   public class BrandAddDto:IDto
    {
		public string Name { get; set; }
		public bool IsFeatured { get; set; }	
		public IFormFile Image { get; set; }
	}
}
