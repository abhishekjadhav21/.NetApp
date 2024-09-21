using App.DAL.Interface;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Middleware
{
	public class AuthorizationHandler : IAuthorizationMiddlewareResultHandler
	{
		private IUserTokenRepository _userTokenRepository;

		public AuthorizationHandler(IUserTokenRepository userTokenRepository)
		{
			_userTokenRepository = userTokenRepository;
		}

		private AuthorizationMiddlewareResultHandler resultHandler = new();

		public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
		{
			if (!(await _userTokenRepository.IsCurrentActiveToken()))
			{
				context.Response.StatusCode = (int)401;
				await context.Response.WriteAsJsonAsync(new { message = "Invalid Token" });
				return;
			}
			if (authorizeResult.Challenged)
			{
				context.Response.StatusCode = (int)401;
				await context.Response.WriteAsJsonAsync(new { message = "un_authorized_access" });
				return;
			}
			if (authorizeResult.Forbidden)
			{
				context.Response.StatusCode = (int)401;
				await context.Response.WriteAsJsonAsync(new { message = "authorized_permission" });
				return;
			}
			await resultHandler.HandleAsync(next, context, policy, authorizeResult);
		}
	}
}
