using Business.Abstract;
using Core.Entities.Concrete;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using Core.Helpers.Security.Hashing;
using Core.Helpers.Security.JWT;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class AuthManager(IUserService userService,ITokenHelper tokenHelper) : IAuthService
	{
		private readonly IUserService _userService = userService;
		private readonly ITokenHelper _tokenHelper = tokenHelper;

		public IDataResult<AccessToken> CreateAccessToken(User user)
		{
			var claims = _userService.GetClaims(user);
			var accessToken = _tokenHelper.CreateAccessToken(user, claims);
			return new SuccessDataResult<AccessToken>(accessToken, "Token uğurla yaradıldı");
		}

		public IDataResult<User> Login(LoginDto loginDto)
		{
			var userToCheck = _userService.GetByMail(loginDto.Email);
			if (userToCheck == null)
			{
				return new ErrorDataResult<User>("istifadeci tapilmadi");
			}

			if (!HashingHelper.VerifiedPasswordHash(loginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
			{
				return new ErrorDataResult<User>("Email ve ya sifre yanliasdir");
			}

			return new SuccessDataResult<User>(userToCheck, "login olundu!");
		}

		public IDataResult<User> Register(RegisterDto registerDto,string password)
		{
			byte[] passwordHash, passwordSalt;
			HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
			var user = new User
			{
				Email = registerDto.Email,
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				PhoneNumber = registerDto.PhoneNumber,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Status = true
			};
			_userService.Add(user);
			return new SuccessDataResult<User>(user, "qeydiyyat ugurla oldu");
		}

		public IResult UserExists(string email)
		{
			if (_userService.GetByMail(email) != null)
			{
				return new ErrorResult("Bu istifadəçi mövcuddur");
			}
			return new SuccessResult();
		}
	}
}
