using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ReviewDtos
{
    public class ReviewAddDto : IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ProductId { get; set; }
    }
}
