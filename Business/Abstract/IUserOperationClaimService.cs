using Core.Helpers.Results.Abstract;
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
	}
}
