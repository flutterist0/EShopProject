﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class CartItemDto:IDto
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
		public string ProductImageUrl { get; set; }
		public decimal? Price { get; set; }
        public decimal? ShippingCost { get; set; }
        public int Quantity { get; set; }
	}
}
