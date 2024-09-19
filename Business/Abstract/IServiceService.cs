using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IServiceService
	{
		IResult Add(Service service);
		IResult Update(Service service, int id);
		IResult Delete(int id);
		IDataResult<List<Service>> GetAll();
		IDataResult<Service> Get(int id);
	}
}
