using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CategoryManager(ICategoryDal categoryDal):ICategoryService
	{
		private readonly ICategoryDal _categoryDal = categoryDal;
		public IResult Add(Category category)
		{
			if (category.Name.Length > 3)
			{
				_categoryDal.Add(category);
				return new SuccessResult("Add olundu");
			}
			else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			Category deleteCategory = null;
			Category result = _categoryDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteCategory = result;

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

		public IResult Update(Category category, int id)
		{
			var updateCategory = _categoryDal.Get(a => a.Id == id && a.IsDelete == false);
			updateCategory.Name = category.Name;
			updateCategory.ImageUrl = category.ImageUrl;
			_categoryDal.Update(category);
			return new SuccessResult();
		}
	}
}
