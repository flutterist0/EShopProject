using Autofac.Core;
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
	public class BrandManager(IBrandDal brandDal) : IBrandService
	{
		private readonly IBrandDal _brandDal = brandDal;
		public IResult Add(Brand brand)
		{
			if (brand.Name.Length > 3)
			{
				_brandDal.Add(brand);
				return new SuccessResult("Add olundu");
			}
			else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			Brand deleteBrand = null;
			Brand result = _brandDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteBrand = result;

				_brandDal.Delete(deleteBrand);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Brand> Get(int id)
		{
			var result = _brandDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<Brand>(result, "loaded");
			else return new ErrorDataResult<Brand>(result, "tapilmadi");
		}

		public IDataResult<List<Brand>> GetAll()
		{
			var result = _brandDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Brand>>(result);
			else return new ErrorDataResult<List<Brand>>("xeta baş verdi");
		}

		public IResult Update(Brand brand, int id)
		{
			var updateBrand = _brandDal.Get(a => a.Id == id && a.IsDelete == false);
			updateBrand.Name = brand.Name;	
			updateBrand.ImageUrl = brand.ImageUrl;

			_brandDal.Update(brand);
			return new SuccessResult();
		}
	}
}
