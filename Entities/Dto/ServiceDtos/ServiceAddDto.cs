using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ServiceDtos
{
    public class ServiceAddDto : IDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile Image { get; set; }
    }
}
