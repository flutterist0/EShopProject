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
	public interface IOperationClaimService
	{
		IResult Add(OperationClaim operationClaim);
		IResult Update(OperationClaim operationClaim,int id);
		IResult Delete(int id);
		IDataResult<List<OperationClaim>> GetAll();
		IDataResult<OperationClaim> Get(int id);
	}
}
