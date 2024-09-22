using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ContactFormManager(IContactFormDal contactFormDal) : IContactFormService
	{
		private readonly IContactFormDal _contactFormDal = contactFormDal;	
		public IResult Add(ContactForm contactForm)
		{
			_contactFormDal.Add(contactForm);
			return new SuccessResult("Contact form successfully added.");
		}
	}
}
