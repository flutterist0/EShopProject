using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IContactService
	{
		IResult Add(Contact contact);
		IResult Update(Contact contact, int id);
		IResult Delete(int id);
		IDataResult<List<Contact>> GetAll();
		IDataResult<Contact> Get(int id);
	}
}
