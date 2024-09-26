using Business.Abstract;
using Core.Helpers.Business;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto.ProductDtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager(IProductDal productDal, IProductImageService productImageService, IAddPhotoHelperService addPhotoHelperService,ICategoryDal categoryDal,IProductImageDal productImageDal) : IProductService
	{
		private readonly IProductDal _productDal =productDal;
		private readonly IProductImageService _productImageService = productImageService;
		private readonly IAddPhotoHelperService _addPhotoHelperService = addPhotoHelperService;
		private readonly ICategoryDal _categoryDal =categoryDal;
		private readonly IProductImageDal _productImageDal = productImageDal;
		public IResult Add(ProductAddDto productAddDto)
		{
			var newProduct = new Product()
			{
				Name = productAddDto.Name,
				Description = productAddDto.Description,
				Spesification = productAddDto.Spesification,
				Price = productAddDto.Price,
				IsDiscount = productAddDto.IsDiscount,
				DiscountPrice = productAddDto.DiscountPrice,
				Stock = productAddDto.Stock,
				Quantity = productAddDto.Quantity,
				IsFeatured = productAddDto.IsFeatured,
				CreatedAt = DateTime.Now,
				CategoryId = productAddDto.CategoryId,
				ProductImages = new List<ProductImage>()
			};
			_productDal.Add(newProduct);
			foreach (var image in productAddDto.ProductImages)
			{
				var productImageAddDto = new ProductImageAddDto
				{
					ProductId = newProduct.Id,  
					Image = image              
				};
				_productImageService.Add(productImageAddDto);
			}
			return new SuccessResult("Added");
		}

		public IResult Delete(int id)
		{
			Product deleteProduct = null;
			Product result = _productDal.Get(a => a.Id == id && a.IsDelete == false);

			if (result != null)
			{
				deleteProduct = result;
				deleteProduct.IsDelete = true;
				_productDal.Delete(deleteProduct);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Product> Get(int id)
		{
			var result = _productDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<Product>(result, "loaded");
			else return new ErrorDataResult<Product>(result, "tapilmadi");
		}

		public IDataResult<List<Product>> GetAll()
		{
			var result = _productDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Product>>(result);
			else return new ErrorDataResult<List<Product>>("xeta baş verdi");
		}



		public IDataResult<ProductDetailDto> GetProductDetails(int productId)
		{
			var product = _productDal.Get(p => p.Id == productId && p.IsDelete == false);

			if (product == null)
			{
				return new ErrorDataResult<ProductDetailDto>("Product not found");
			}
			var category = _categoryDal.Get(c => c.Id == product.CategoryId);

			var productImages = _productImageDal.GetAll(pi => pi.ProductId == product.Id).ToList();

			var productDetailsDto = new ProductDetailDto
			{
				Name = product.Name,
				Price = product.Price,
				DiscountPrice = product.IsDiscount ? product.DiscountPrice : null,
				Description = product.Description,
				Spesification = product.Spesification,
				CategoryName = category?.Name, 
				Images = productImages.Select(img => img.ImageUrl).ToList()
			};

			return new SuccessDataResult<ProductDetailDto>(productDetailsDto);
		}

		public IDataResult<List<ProductListDto>> GetProductList()
		{
			var result = _productDal.GetAllProducts();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<ProductListDto>>(result);
			}
			else
				return new ErrorDataResult<List<ProductListDto>>("xeta bas verdi");
		}

		public IDataResult<List<ProductToCategoryListDto>> GetProductsByCategory(int categoryId)
		{
			var products = _productDal.GetAll(p => p.CategoryId == categoryId && p.IsDelete == false).ToList();
			if (products == null || products.Count == 0)
			{
				return new ErrorDataResult<List<ProductToCategoryListDto>>("No products found for this category.");
			}

			var productListDto = products.Select(p =>
			{
				var productImages = _productImageDal.GetAll(pi => pi.ProductId == p.Id).ToList();
				var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl?? "defaultimage-url"; ;

				return new ProductToCategoryListDto
				{
					Name = p.Name,
					Price = p.Price,
					IsDiscount = p.IsDiscount,
					DiscountPrice = p.IsDiscount ? p.DiscountPrice : null,
					ImageUrl = firstImageUrl 
				};
			}).ToList();


			return new SuccessDataResult<List<ProductToCategoryListDto>>(productListDto, "Products retrieved successfully.");
		}

		public IDataResult<List<ProductToCategoryListDto>> GetNewestProducts()
		{
			var products = _productDal.GetAll(p => p.IsDelete == false)
							 .OrderByDescending(p => p.CreatedAt)
							 .ToList();

			if (products == null || products.Count == 0)
			{
				return new ErrorDataResult<List<ProductToCategoryListDto>>("No products found.");
			}

			var productListDto = products.Select(p =>
			{
				var productImages = _productImageDal.GetAll(pi => pi.ProductId == p.Id).ToList();
				var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl?? "defaultimage-url"; ;

				return new ProductToCategoryListDto
				{
					Name = p.Name,
					Price = p.Price,
					IsDiscount = p.IsDiscount,
					DiscountPrice = p.IsDiscount ? p.DiscountPrice : null,
					ImageUrl = firstImageUrl 
				};
			}).ToList();

			return new SuccessDataResult<List<ProductToCategoryListDto>>(productListDto, "Products retrieved successfully.");
		}

        public IResult Update(ProductUpdateDto productUpdateDto, List<int> deleteImageIds)
        {
            var existingProduct = _productDal.Get(p => p.Id == productUpdateDto.ProductId && p.IsDelete == false);

            if (existingProduct == null)
            {
                return new ErrorResult("Product not found");
            }

            // Məhsul məlumatlarını yenilə
            existingProduct.Name = productUpdateDto.Name;
            existingProduct.Description = productUpdateDto.Description;
            existingProduct.Price = productUpdateDto.Price;
            existingProduct.Stock = productUpdateDto.Stock;
            existingProduct.Quantity = productUpdateDto.Quantity;
            existingProduct.IsDiscount = productUpdateDto.IsDiscount;
            existingProduct.DiscountPrice = productUpdateDto.IsDiscount ? productUpdateDto.DiscountPrice : 0;
			existingProduct.CategoryId = productUpdateDto.CategoryId;
			existingProduct.IsFeatured = productUpdateDto.IsFeatured;
			existingProduct.Id = productUpdateDto.ProductId;

            _productDal.Update(existingProduct);

            // Silinməli olan şəkilləri sil
            foreach (var imageId in deleteImageIds)
            {
                var image = _productImageDal.Get(i => i.Id == imageId);
                _productImageDal.Delete(image);
            }

            // Yeni şəkilləri əlavə et
            foreach (var imageFile in productUpdateDto.ProductImages)
            {
                var productImageAddDto = new ProductImageAddDto
                {
                    ProductId = existingProduct.Id,
                    Image = imageFile
                };
                _productImageService.Add(productImageAddDto);
            }

            return new SuccessResult("Product updated successfully");
        }


        public IDataResult<List<ProductToCategoryListDto>> GetProductsSortedByPriceAscending()
		{
			var products = _productDal.GetAll(p => p.IsDelete == false)
							  .OrderBy(p => p.IsDiscount ? p.DiscountPrice : p.Price)
							  .ToList();

			if (products == null || products.Count == 0)
			{
				return new ErrorDataResult<List<ProductToCategoryListDto>>("No products found.");
			}

			var productListDto = products.Select(p =>
			{
				var productImages = _productImageDal.GetAll(pi => pi.ProductId == p.Id).ToList();
				var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl??"defaultimage-url";

				return new ProductToCategoryListDto
				{
					Name = p.Name,
					Price = p.IsDiscount ? p.DiscountPrice : p.Price, 
					IsDiscount = p.IsDiscount,
					DiscountPrice = p.IsDiscount ? p.DiscountPrice : null,
					ImageUrl = firstImageUrl
				};
			}).ToList();

			return new SuccessDataResult<List<ProductToCategoryListDto>>(productListDto, "Products retrieved successfully.");
		}

		public IDataResult<List<ProductToCategoryListDto>> GetProductsSortedByPriceDescending()
		{
			var products = _productDal.GetAll(p => p.IsDelete == false)
							 .OrderByDescending(p => p.IsDiscount ? p.DiscountPrice : p.Price)
							 .ToList();

			if (products == null || products.Count == 0)
			{
				return new ErrorDataResult<List<ProductToCategoryListDto>>("No products found.");
			}

			var productListDto = products.Select(p =>
			{
				var productImages = _productImageDal.GetAll(pi => pi.ProductId == p.Id).ToList();
				var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl ?? "defaultimage-url";

				return new ProductToCategoryListDto
				{
					Name = p.Name,
					Price = p.IsDiscount ? p.DiscountPrice : p.Price, 
					IsDiscount = p.IsDiscount,
					DiscountPrice = p.IsDiscount ? p.DiscountPrice : null,
					ImageUrl = firstImageUrl
				};
			}).ToList();

			return new SuccessDataResult<List<ProductToCategoryListDto>>(productListDto, "Products retrieved successfully.");
		}

		public IDataResult<PagedResult<ProductToCategoryListDto>> GetProductsByCategoryWithPagination(int categoryId, int pageNumber, int pageSize)
		{
			var totalProducts = _productDal.GetAll(p => p.CategoryId == categoryId && p.IsDelete == false).Count();

			var products = _productDal.GetAll(p => p.CategoryId == categoryId && p.IsDelete == false)
									  .OrderBy(p => p.Name)
									  .Skip((pageNumber - 1) * pageSize)
									  .Take(pageSize)
									  .ToList();

			if (products == null || products.Count == 0)
			{
				return new ErrorDataResult<PagedResult<ProductToCategoryListDto>>("No products found for this category.");
			}

			var productListDto = products.Select(p =>
			{
				var productImages = _productImageDal.GetAll(pi => pi.ProductId == p.Id).ToList();
				var firstImageUrl = productImages.FirstOrDefault()?.ImageUrl ?? "default-image-url";

				return new ProductToCategoryListDto
				{
					Name = p.Name,
					Price = p.IsDiscount ? p.DiscountPrice : p.Price,
					IsDiscount = p.IsDiscount,
					DiscountPrice = p.IsDiscount ? p.DiscountPrice : null,
					ImageUrl = firstImageUrl
				};
			}).ToList();

			var pagedResult = new PagedResult<ProductToCategoryListDto>
			{
				Items = productListDto,
				TotalCount = totalProducts,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize)
			};

			return new SuccessDataResult<PagedResult<ProductToCategoryListDto>>(pagedResult, "Products retrieved successfully.");
		}

   
    }
}
		