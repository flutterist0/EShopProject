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
	public class CountryManager(ICountryDal countryDal):ICountryService
	{
		private readonly ICountryDal _countryDal = countryDal;
		public IResult Add(Country country)
		{
			if (country.Name.Length > 3)
			{
				_countryDal.Add(country);
				return new SuccessResult("Add olundu");
			}
			else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			Country deleteCountry = null;
			Country result = _countryDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteCountry = result;

				_countryDal.Delete(deleteCountry);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Country> Get(int id)
		{
			var result = _countryDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<Country>(result, "loaded");
			else return new ErrorDataResult<Country>(result, "tapilmadi");
		}

		public IDataResult<List<Country>> GetAll()
		{
			var result = _countryDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Country>>(result);
			else return new ErrorDataResult<List<Country>>("xeta baş verdi");
		}

		public IResult Update(Country country, int id)
		{
			var updateCountry = _countryDal.Get(a => a.Id == id && a.IsDelete == false);
			updateCountry.Name = country.Name;
			_countryDal.Update(updateCountry);
			return new SuccessResult();
		}
	}
}
