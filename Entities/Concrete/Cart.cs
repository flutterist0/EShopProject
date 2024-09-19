using Core.Entities.Concrete;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Cart:BaseEntity
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int Count { get; set; }
		public int UserId { get; set; }
        public User User { get; set; }
		public double Price { get; set; }
	}
}
