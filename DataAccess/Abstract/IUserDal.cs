using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IUserDal:IBaseRepository<User>
	{
		List<OperationClaim> GetClaims(User user);
		UserGetDto GetUserById(int userId);
		List<UserOperationClaimDto> GetAllUsers();
	}
}
