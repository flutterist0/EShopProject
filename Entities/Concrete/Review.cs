using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Review:BaseEntity
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
		public int Rating { get; set; }
		public DateTime ReviewDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
