using Core.Entities.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto.ProductDtos
{
    public class ProductUpdateDto : IDto
    {
        public int ProductId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Spesification { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> ProductImages { get; set; }
        public List<ProductImage> ExistingImages { get; set; }
    }
}
