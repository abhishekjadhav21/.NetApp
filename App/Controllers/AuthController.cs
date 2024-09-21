using App.DAL.Interface;
using App.DAL.Model;
using App.DAL.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public readonly IAuthRepository _authRepository;

		public AuthController(IAuthRepository authRepository)
		{
			_authRepository = authRepository;

		}
		[HttpPost]
		public async Task<IActionResult> RegisterUser([FromForm] UserRegisterRequest userRegisterRequest)
		{
			try
			{
				var user = await _authRepository.RegisterUser(userRegisterRequest);
				if (user != null)
				{
					return Ok(user);
				}
				else
				{
					return BadRequest("Internal Server error ");
				}
			}
			catch (Exception ex) 
			{
			   throw (ex);
			
			}

		}
	}
}
