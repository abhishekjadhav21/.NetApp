using App.DAL.Model;
using App.DAL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Interface
{
	public interface IAuthRepository
	{
		Task<User> RegisterUser(UserRegisterRequest userRegisterRequest);
	}
}
