using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICountryService
	{
		IResult Add(Country country);
		IResult Update(Country country, int id);
		IResult Delete(int id);
		IDataResult<List<Country>> GetAll();
		IDataResult<Country> Get(int id);
	}
}
