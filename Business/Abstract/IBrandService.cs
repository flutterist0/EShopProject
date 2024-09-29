using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IBrandService
	{
		IResult Add(BrandAddDto brand);
		IResult Update(Brand brand, int id);
		IResult Delete(int id);
		IDataResult<List<BrandWithProductsDto>> GetAllBrandsWithProducts();
		IDataResult<List<Brand>> GetAll();
		IDataResult<Brand> Get(int id);
	}
}
