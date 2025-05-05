using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal,IUserDal userDal,IOperationClaimDal operationClaimDal) : IUserOperationClaimService
	{
		private readonly IUserOperationClaimDal _userOperationClaimDal = userOperationClaimDal;
        private readonly IUserDal _userDal = userDal;
        private readonly IOperationClaimDal _operationClaimDal = operationClaimDal;
        //[SecuredOperation("Admin")]
        public IResult Add(int userId, int operationClaimId)
		{
			_userOperationClaimDal.Add(new UserOperationClaim() { UserId = userId, OperationClaimId = operationClaimId });
			return new SuccessResult("Added");
		}
        //[SecuredOperation("Admin")]
        public IResult Delete(int userId,int operationClaimId)
        {
            UserOperationClaim deleteUserOperationClaim = null;
            UserOperationClaim result = _userOperationClaimDal.Get(uo => uo.UserId==userId &&uo.OperationClaimId==operationClaimId);
            if (result != null)
            {
                deleteUserOperationClaim = result;
                _userOperationClaimDal.DeleteX(deleteUserOperationClaim);
                return new SuccessResult("deleted");
            }
            else

                return new ErrorResult("silinmedi");
        }

        public IDataResult<UserOperationClaimDto> GetUserOperationClaimsById(int userId)
        {
            var result = _userOperationClaimDal.Get(ue => ue.UserId == userId);
            var user = _userDal.GetUserById(userId);
            var operationClaim = _operationClaimDal.Get(o=>o.Id==result.OperationClaimId);
            if (result != null)
            {
                var userOperationClaimDto = new UserOperationClaimDto
                {
                    UserId = result.UserId,
                    OperationClaimId = result.OperationClaimId,
                    OperationClaimName = operationClaim.Name,
                    FirstName = user.FirstName, 
                    LastName = user.LastName,   
                    Email = user.Email,   
                    PhoneNumber = user.PhoneNumber,
                };

                return new SuccessDataResult<UserOperationClaimDto>(userOperationClaimDto, "loaded");
            }
            else
            {
                return new ErrorDataResult<UserOperationClaimDto>(null, "tapilmadi");
            }
        }
    }
    }

