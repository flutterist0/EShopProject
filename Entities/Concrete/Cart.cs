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
		public int UserId { get; set; } 
		public User User { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public ICollection<CartItem> CartItems { get; set; } 
	}
}
