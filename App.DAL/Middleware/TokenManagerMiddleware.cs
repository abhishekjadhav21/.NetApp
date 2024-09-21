using App.DAL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Middleware
{
	public class TokenManagerMiddleware : IMiddleware
	{
		private readonly IUserTokenRepository _iUserTokenRepository;

		public TokenManagerMiddleware(IUserTokenRepository userTokenRepository)
		{
			_iUserTokenRepository = userTokenRepository;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				await next(context);
				return;
			}
			if (await _iUserTokenRepository.IsCurrentActiveToken())
			{
				await next(context);
				return;
			}
			context.Response.StatusCode = (int)401;
			await context.Response.WriteAsJsonAsync(new { message = "Unauthorised" });
			return;
		}
	}

	public class AuthorizationOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (context.MethodInfo.DeclaringType != null)
			{
				var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
									.Union(context.MethodInfo.GetCustomAttributes(true))
									.OfType<AuthorizeAttribute>();

				if (attributes != null && attributes.Count() > 0)
				{
					var attr = attributes.ToList()[0];

					operation.Responses.Add("422", new OpenApiResponse { Description = "Unprocessable Entity" });
					operation.Responses.Add("400", new OpenApiResponse { Description = "BadRequest" });
					operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
					operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
					operation.Responses.Add("404", new OpenApiResponse { Description = "NotFound" });

					IList<string> securityInfos = new List<string>();
					securityInfos.Add($"{nameof(AuthorizeAttribute.Policy)}:{attr.Policy}");
					securityInfos.Add($"{nameof(AuthorizeAttribute.Roles)}:{attr.Roles}");
					securityInfos.Add($"{nameof(AuthorizeAttribute.AuthenticationSchemes)}:{attr.AuthenticationSchemes}");

					operation.Security = new List<OpenApiSecurityRequirement>()
					{
						new OpenApiSecurityRequirement()
						{
							{
								new OpenApiSecurityScheme
								{
									Reference = new OpenApiReference
									{
										Type = ReferenceType.SecurityScheme,
										Id = JwtBearerDefaults.AuthenticationScheme
									}
								},
							  new string[]{}
							}
						}
					};
				}
			}
		}
	}
}
