using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using Core.Helpers.Security.Hashing;
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

        public IDataResult<User> ChangePassword(ChangePasswordDto changePasswordDto, int userId)
        {
            var user = _userDal.Get(u=>u.Id==userId);
            if (user == null)
            {
                return new ErrorDataResult<User>("İstifadəçi tapılmadı.");
            }
            if (!HashingHelper.VerifiedPasswordHash(changePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>("Cari parol yanlışdır.");
            }
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return new ErrorDataResult<User>("Yeni parol təsdiq paroluna uyğun deyil.");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);

            return new SuccessDataResult<User>(user, "Parol uğurla dəyişdirildi.");
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
