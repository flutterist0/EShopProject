using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class OperationClaimManager(IOperationClaimDal operationClaimDal) : IOperationClaimService
	{
		private readonly IOperationClaimDal _operationClaimDal = operationClaimDal;
		public IResult Add(OperationClaim operationClaim)
		{
			if (operationClaim.Name.Length > 3)
			{
				_operationClaimDal.Add(operationClaim);
				return new SuccessResult("Add olundu");
			} else
				return new ErrorResult("ELave edilmedi");
		}

		public IResult Delete(int id)
		{
			OperationClaim deleteOperationClaim = null;
			OperationClaim result = _operationClaimDal.Get(a => a.Id == id);
			if (result != null)
			{
				deleteOperationClaim = result;
			
				_operationClaimDal.Delete(deleteOperationClaim);
				Console.WriteLine("deleted");
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<List<OperationClaim>> GetAll()
		{
			var result = _operationClaimDal.GetAll().ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<OperationClaim>>(result);
			else return new ErrorDataResult<List<OperationClaim>>("xeta baş verdi");
		}

		public IDataResult<OperationClaim> Get(int id)
		{
			var result = _operationClaimDal.Get(t => t.Id == id);
			if (result != null)
				return new SuccessDataResult<OperationClaim>(result, "loaded");
			else return new ErrorDataResult<OperationClaim>(result, "tapilmadi");
		}

		public IResult Update(OperationClaim operationClaim,int id)
		{
			var updateOperationClaim = _operationClaimDal.Get(a => a.Id == id);
			updateOperationClaim.Name = operationClaim.Name;

			_operationClaimDal.Update(updateOperationClaim);
			return new SuccessResult();
		}
	}
}
