using Business.Abstract;
using Core.Helpers.Business;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CategoryManager(ICategoryDal categoryDal, IAddPhotoHelperService addPhotoHelperService) :ICategoryService
	{
		private readonly ICategoryDal _categoryDal = categoryDal;
		private readonly IAddPhotoHelperService _addPhotoHelperService = addPhotoHelperService;
		public IResult Add(CategoryAddDto categoryDto)
		{
			var guid = Guid.NewGuid() + "-" + categoryDto.Image.FileName;
			_addPhotoHelperService.AddImage(categoryDto.Image, guid);
			Category category = new()
			{
				Name = categoryDto.Name,
				ImageUrl = "/images/" + guid,
				IsFeatuerd = categoryDto.IsFeatuerd,
			};
			_categoryDal.Add(category);
			return new SuccessResult("Elave olundu");
		}

		public IResult Delete(int id)
		{
			Category deleteCategory = null;
			Category result = _categoryDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteCategory = result;
				deleteCategory.IsDelete = true;
				_categoryDal.Delete(deleteCategory);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Category> Get(int id)
		{
			var result = _categoryDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<Category>(result, "loaded");
			else return new ErrorDataResult<Category>(result, "tapilmadi");
		}

		public IDataResult<List<Category>> GetAll()
		{
			var result = _categoryDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Category>>(result);
			else return new ErrorDataResult<List<Category>>("xeta baş verdi");
		}

		public IDataResult<List<CategoryWithProductsDto>> GetAllCategoriesWithProducts()
		{
			var result = _categoryDal.GetAllCategoriesWithProducts();
            if (result.Count>0)
            {
				return new SuccessDataResult<List<CategoryWithProductsDto>>(result);
            }else
				return new ErrorDataResult<List<CategoryWithProductsDto>>(result,"xeta");
        }

        public IDataResult<List<Category>> GetAllIsFeatured()
        {
            var result = _categoryDal.GetAll(c=>c.IsDelete==false&&c.IsFeatuerd==true).Take(3).ToList();
            if (result.Count>0)
                return new SuccessDataResult<List<Category>>(result);
            else return new ErrorDataResult<List<Category>>("xeta baş verdi");
        }

        public IResult Update(Category category, int id)
		{
			var updateCategory = _categoryDal.Get(a => a.Id == id && a.IsDelete == false);
			updateCategory.Name = category.Name;
			updateCategory.ImageUrl = category.ImageUrl;
			_categoryDal.Update(updateCategory);
			return new SuccessResult();
		}
	}
}
