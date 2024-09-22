using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class SocialNetworkAddDto:IDto
	{
		public string Name { get; set; }
		public IFormFile Image { get; set; }
	}
}
