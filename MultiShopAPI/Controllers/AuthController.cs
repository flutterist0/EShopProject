using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dto;
using Entities.Dto.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController(IAuthService authService,IUserService userService) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
		private readonly IUserService _userService = userService;
		[HttpPost("register")]
		public ActionResult Register(RegisterDto userForRegisterDto)
		{
			var userExists = _authService.UserExists(userForRegisterDto.Email);
			if (!userExists.Success)
			{
				return BadRequest(userExists.Message);
			}

			var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
			var result = _authService.CreateAccessToken(registerResult.Data);
			if (result.Success)
			{
				return Ok(result.Data);
			}

			return BadRequest(result.Message);
		}

		[HttpPost("login")]
		public ActionResult Login(LoginDto loginDto)
		{
			var userToLogin = _authService.Login(loginDto);
			if (!userToLogin.Success)
			{
				return BadRequest(userToLogin.Message);
			}
			var result = _authService.CreateAccessToken(userToLogin.Data);
			if (result.Success)
			{
				return Ok(result.Data);
			}
			else
				return BadRequest(result.Message);
		}

		[HttpGet("getUserByUserId")]
		public IActionResult GetUserByUserId(int userId)
		{
			var result = _userService.GetById(userId);
            if (result.Success)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }

		[HttpPut]
		public IActionResult Update(UserUpdateDto userUpdateDto, int userId)
		{
			var result = _userService.Update(userUpdateDto, userId);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

		[HttpPut("changePassword")]
		public IActionResult ChangePassword(ChangePasswordDto changePasswordDto, int userId)
		{
			var result = _userService.ChangePassword(changePasswordDto, userId);
			if (result.Success)
			{
				return Ok(result);
			}else
				return BadRequest(result);
		}

		[HttpGet("getAllUsers")]
		public IActionResult GetAllUsers()
		{
			var result = _userService.GetUsersWithOperationClaim();
            if (result.Count>0)
            {
				return Ok(result);
            }else
				return BadRequest(result);
        }
	}
}
