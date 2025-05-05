using Core.Helpers.Results.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserOperationClaimService
	{
		IResult Add(int userId, int operationClaimId);
		IResult Delete(int userId, int operationClaimId);
        IDataResult<UserOperationClaimDto> GetUserOperationClaimsById(int userId);

    }
}
