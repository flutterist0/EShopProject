using Business.Abstract;
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
	public class UserManager(IUserDal userDal) : IUserService
	{
		private readonly IUserDal _userDal=userDal;
		public void Add(User user)
		{
            _userDal.Add(user);
        }

        public IDataResult<UserGetDto> GetById(int userId)
        {
            var result = _userDal.GetUserById(userId);
            if (result != null)
                return new SuccessDataResult<UserGetDto>(result, "loaded");
            else return new ErrorDataResult<UserGetDto>(result, "tapilmadi");
        }

        public User GetByMail(string email)
		{
			return _userDal.Get(u => u.Email == email);
		}

		public List<OperationClaim> GetClaims(User user)
		{
			return _userDal.GetClaims(user);
		}

        public IResult Update(UserUpdateDto userUpdateDto, int userId)
        {
            var exsitingUser = _userDal.Get(u=>u.Id==userId);
            if (exsitingUser == null)
            {
                return new ErrorResult("user not found");
            }
            exsitingUser.Id = userUpdateDto.UserId;
            exsitingUser.FirstName = userUpdateDto.FirstName;
            exsitingUser.LastName = userUpdateDto.LastName;
            exsitingUser.Email = userUpdateDto.Email;
            exsitingUser.PhoneNumber = userUpdateDto.PhoneNumber;
            _userDal.Update(exsitingUser);
            return new SuccessResult("User succesfully updated");
        }
    }
}
