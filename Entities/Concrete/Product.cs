using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Product:BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }
        public string Spesification { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
