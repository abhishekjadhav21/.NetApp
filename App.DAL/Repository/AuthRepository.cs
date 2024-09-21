using App.DAL.DataBase;
using App.DAL.Interface;
using App.DAL.Model;
using App.DAL.Requests;
using App.DAL.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repository
{
	public class AuthRepository : IAuthRepository
	{
		private readonly IConfiguration _configuration;
		private readonly DataContext _dataContext;

		public AuthService _authService;
		public AuthRepository(DataContext dataContext,IConfiguration configuration) 
		{
			_configuration = configuration;
			_dataContext = dataContext;
			_authService = new AuthService(_dataContext);
		
		}

		async Task<User> IAuthRepository.RegisterUser(UserRegisterRequest userRegisterRequest)
		{
			return await _authService.Registeruser(userRegisterRequest);	
		}
	}
}
