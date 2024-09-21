using App.DAL.DataBase;
using App.DAL.Interface;
using App.DAL.Model;
using App.DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repository
{
	public class UserTokenRepository :IUserTokenRepository
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserTokenService _userTokenService;
		public DataContext _dataContext;
		public UserTokenRepository(IConfiguration configuration,IHttpContextAccessor httpContextAccessor,DataContext dataContext) 
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_dataContext = dataContext;
			_userTokenService = new UserTokenService(_configuration,_httpContextAccessor,_dataContext);
		}

		public async Task<UserToken> Create(int UserId, string Role, string Email, string FullName)
		{
			return await _userTokenService.Create(UserId, Role, Email, FullName);
		}

		public UserToken  GenerateToken(int UserId, string Role, string Email, string FullName)
		{
			return  _userTokenService.GenerateToken(UserId, Role, Email, FullName);	
		}

		public string GetCurrentAsync()
		{
			return  _userTokenService.GetCurrentAsync();
		}

		public async Task<UserToken> GetToken(string Token)
		{
			return await _userTokenService.GetToken(Token);
		}

		public Task<bool> IsCurrentActiveToken()
		{
			return _userTokenService.IsCurrentActiveToken();
		}

		public async Task<UserToken> Remove(UserToken userToken)
		{
			return await _userTokenService.Remove(userToken);
		}
	}
}
