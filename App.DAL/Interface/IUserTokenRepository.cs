using App.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Interface
{
	public interface IUserTokenRepository
	{
		public Task<UserToken> Create(int UserId, string Role, string Email, string FullName);

		public Task<UserToken> GetToken(string Token);

		public UserToken GenerateToken(int UserId, string Role, string Email, string FullName);

		public Task<bool> IsCurrentActiveToken();

		public string GetCurrentAsync();

		public Task<UserToken> Remove(UserToken userToken);
	}
}
