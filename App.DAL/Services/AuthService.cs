using App.DAL.DataBase;
using App.DAL.Model;
using App.DAL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Services
{
	public class AuthService
	{
		private readonly DataContext dataContext;
		public AuthService(DataContext dataContext) 
		{ 
			this.dataContext = dataContext;
		}

		public async Task<User> Registeruser(UserRegisterRequest userRegisterRequest)
		{
			var User = new User
			{
				FirstName = userRegisterRequest.FirstName,
				LastName = userRegisterRequest.LastName,
				Email = userRegisterRequest.Email,
				Mobile = userRegisterRequest.Mobile,
				Password = BCrypt.Net.BCrypt.HashPassword(userRegisterRequest.Password),
				Role = userRegisterRequest.Role,
			};
			var saveUser = await dataContext.Users.AddAsync(User);
			await dataContext.SaveChangesAsync();
			return saveUser.Entity;
		}
	}

}
