using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IPaymentMethodService
	{
		IResult Add(PaymentMethod paymentMethod);
		IResult Update(PaymentMethod paymentMethod, int id);
		IResult Delete(int id);
		IDataResult<List<PaymentMethod>> GetAll();
		IDataResult<PaymentMethod> Get(int id);
	}
}
