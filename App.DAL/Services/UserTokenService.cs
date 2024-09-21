using App.DAL.DataBase;
using App.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Services
{
	public class UserTokenService
	{
		public IConfiguration _configuration;
		public IHttpContextAccessor _httpContextAccessor;
		public DataContext _dataContext;
		public UserTokenService(IConfiguration configuration,IHttpContextAccessor httpContextAccessor,DataContext dataContext) 
		{ 
		
			_configuration = configuration;
		    _httpContextAccessor = httpContextAccessor;
			_dataContext = dataContext;
		}

		public async Task<UserToken> Create(int UserId, string Role, string Email, string FullName)
		{
			UserToken userToken =  this.GenerateToken(UserId, Role, Email, FullName);
			userToken.UserId = UserId;
			userToken.Role = Role;
			var user_Token = _dataContext.UserTokens.Add(userToken);
			var saveToken = user_Token.Entity;
			await _dataContext.SaveChangesAsync();

			return saveToken;
		}

		public async Task<UserToken> GetToken(string Token)
		{
			return await _dataContext.UserTokens.FirstOrDefaultAsync(ut => ut.Token == Token);
		}

		public  UserToken GenerateToken(int UserId, string Role, string Email, string FullName)
		{
			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				new Claim("UserId", UserId.ToString()),
				new Claim(ClaimTypes.Role, Role.ToString()),
				new Claim(ClaimTypes.Email, Email),
				new Claim(ClaimTypes.Name, FullName)
			};

			DateTime notBefore = DateTime.UtcNow;
			DateTime expires = notBefore.AddYears(Int32.Parse(_configuration["Jwt:TokenExpirationYear"]));
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				notBefore,
				expires,
				signingCredentials: signIn
			);
			UserToken userToken = new UserToken();
			userToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
			userToken.ExpiredDate = expires;
			return userToken;
		}

		public async Task<bool> IsCurrentActiveToken()
		{
			var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
			if (!string.IsNullOrEmpty(authorizationHeader))
			{
				var jwtEncodedString = authorizationHeader[7..];
				UserToken user = await this.GetToken(jwtEncodedString);
				if (user == null)
				{
					return false;
				}
				if (user.ExpiredDate <= DateTime.UtcNow)
				{
					await this.Remove(user);
					return false;
				}
			}
			return true;
		}

		public string GetCurrentAsync()
		{
			var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

			return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Replace("Bearer ", "");
		}

		public async Task<UserToken> Remove(UserToken userToken)
		{
			_dataContext.UserTokens.Remove(userToken);
			await _dataContext.SaveChangesAsync();
			return userToken;
		}
	}

}
