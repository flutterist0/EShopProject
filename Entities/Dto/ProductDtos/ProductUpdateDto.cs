using Core.Entities.Abstract;
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
        public string Name { get; set; } // Ürün adı
        public string Description { get; set; } // Ürün açıklaması
        public string Spesification { get; set; } // Ürün spesifikasyonu
        public decimal Price { get; set; } // Ürün fiyatı
        public bool IsDiscount { get; set; } // İndirim durumu
        public decimal DiscountPrice { get; set; } // İndirimli fiyat
        public int Stock { get; set; } // Stok durumu
        public int Quantity { get; set; } // Ürün miktarı
        public bool IsFeatured { get; set; } // Öne çıkan ürün durumu
        public int CategoryId { get; set; } // Ürün kategorisi ID'si
        public List<IFormFile> ProductImages { get; set; } // Ürün resimleri
    }
}
