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
	public class ContactManager(IContactDal contactDal):IContactService
	{
		private readonly IContactDal _contactDal = contactDal;
		public IResult Add(Contact contact)
		{
			if (contact.Description.Length > 3)
			{
				_contactDal.Add(contact);
				return new SuccessResult("Add olundu");
			}
			else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			Contact deleteContact = null;
			Contact result = _contactDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteContact = result;

				_contactDal.Delete(deleteContact);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Contact> Get(int id)
		{
			var result = _contactDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<Contact>(result, "loaded");
			else return new ErrorDataResult<Contact>(result, "tapilmadi");
		}

		public IDataResult<List<Contact>> GetAll()
		{
			var result = _contactDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Contact>>(result);
			else return new ErrorDataResult<List<Contact>>("xeta baş verdi");
		}

		public IResult Update(Contact contact, int id)
		{
			var updateContact = _contactDal.Get(a => a.Id == id && a.IsDelete == false);
			updateContact.Address = contact.Address;	
			updateContact.PhoneNumber = contact.PhoneNumber;	
			updateContact.Email = contact.Email;	
			updateContact.Description = contact.Description;	
			_contactDal.Update(updateContact);
			return new SuccessResult();
		}
	}
}
