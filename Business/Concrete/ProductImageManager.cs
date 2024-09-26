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
    public class ProductImageManager(IProductImageDal productImageDal, IAddPhotoHelperService addPhotoHelperService) : IProductImageService
	{
		private readonly IProductImageDal _productImageDal = productImageDal;
		private readonly IAddPhotoHelperService _addPhotoHelperService = addPhotoHelperService;
		public IResult Add(ProductImageAddDto productImageAddDto)
		{
			var guid = Guid.NewGuid() + "-" + productImageAddDto.Image.FileName;
			_addPhotoHelperService.AddImage(productImageAddDto.Image, guid);
			ProductImage productImage = new()
			{
				ProductId = productImageAddDto.ProductId,
				ImageUrl = "/images/" + guid,
			};
			_productImageDal.Add(productImage);
			return new SuccessResult("Elave olundu");
		}

        public List<ProductImage> GetProductImages(int productId)
        {
            return _productImageDal.GetAll(pi => pi.ProductId == productId && pi.IsDelete==false);
        }
    }
}
