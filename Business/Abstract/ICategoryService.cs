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
	public interface ICategoryService
	{
		IResult Add(CategoryAddDto categoryDto);
		IResult Update(Category category, int id);
		IResult Delete(int id);
		IDataResult<List<Category>> GetAll();
		IDataResult<Category> Get(int id);
	}
}
