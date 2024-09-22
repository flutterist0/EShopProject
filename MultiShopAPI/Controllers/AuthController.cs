using Business.Abstract;
using Entities.Dto.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController(IAuthService authService) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
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
	}
}
