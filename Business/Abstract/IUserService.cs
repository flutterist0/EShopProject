using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserService
	{
		List<OperationClaim> GetClaims(User user);
		void Add(User user);
		User GetByMail(string email);
		IResult Update(UserUpdateDto userUpdateDto, int userId);
		IDataResult<UserGetDto> GetById(int userId);
		User GetUserById(int userId);
		IDataResult<User> ChangePassword(ChangePasswordDto changePasswordDto, int userId);
		List<UserOperationClaimDto> GetUsersWithOperationClaim();
		IDataResult<List<User>> GetAll();

    }
}
