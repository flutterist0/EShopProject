using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal) : IUserOperationClaimService
	{
		private readonly IUserOperationClaimDal _userOperationClaimDal = userOperationClaimDal;
		public IResult Add(int userId, int operationClaimId)
		{
			_userOperationClaimDal.Add(new UserOperationClaim() { UserId = userId, OperationClaimId = operationClaimId });
			return new SuccessResult("Added");
		}
	}
}
