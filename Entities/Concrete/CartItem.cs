﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class CartItem:BaseEntity
	{
		public int CartId { get; set; }
		public Cart Cart { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
        public decimal? ShippingCost { get; set; }
        public int Quantity { get; set; } 
		public decimal? Price { get; set; }
		public DateTime AddedAt { get; set; } = DateTime.Now;
	}
}
