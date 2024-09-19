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
	public class PaymentMethodManager(IPaymentMethodDal paymentMethodDal):IPaymentMethodService
	{
		private readonly IPaymentMethodDal _paymentMethodDal = paymentMethodDal;
		public IResult Add(PaymentMethod paymentMethod)
		{
			if (paymentMethod.MethodName.Length > 3)
			{
				_paymentMethodDal.Add(paymentMethod);
				return new SuccessResult("Add olundu");
			}
			else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			PaymentMethod deletePaymentMethod = null;
			PaymentMethod result = _paymentMethodDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deletePaymentMethod = result;

				_paymentMethodDal.Delete(deletePaymentMethod);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<PaymentMethod> Get(int id)
		{
			var result = _paymentMethodDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<PaymentMethod>(result, "loaded");
			else return new ErrorDataResult<PaymentMethod>(result, "tapilmadi");
		}

		public IDataResult<List<PaymentMethod>> GetAll()
		{
			var result = _paymentMethodDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<PaymentMethod>>(result);
			else return new ErrorDataResult<List<PaymentMethod>>("xeta baş verdi");
		}

		public IResult Update(PaymentMethod paymentMethod, int id)
		{
			var updatePaymentMethod = _paymentMethodDal.Get(a => a.Id == id && a.IsDelete == false);
			updatePaymentMethod.MethodName = paymentMethod.MethodName;
			_paymentMethodDal.Update(paymentMethod);
			return new SuccessResult();
		}
	}
}
