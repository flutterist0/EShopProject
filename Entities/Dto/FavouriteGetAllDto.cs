﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class FavouriteGetAllDto:IDto
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
		public decimal? Price { get; set; }
        public bool IsDiscount { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ImageUrl { get; set; } 
		public int Quantity { get; set; } 
	}
}
