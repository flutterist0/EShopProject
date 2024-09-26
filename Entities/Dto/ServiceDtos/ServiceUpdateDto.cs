using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ServiceDtos
{
    public class ServiceUpdateDto:IDto
    {
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
