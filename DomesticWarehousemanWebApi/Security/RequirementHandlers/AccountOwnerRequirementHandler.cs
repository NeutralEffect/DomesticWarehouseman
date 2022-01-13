using DomesticWarehousemanWebApi.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.RequirementHandlers
{
	public class AccountOwnerRequirementHandler : AuthorizationHandler<AccountOwnerRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AccountOwnerRequirementHandler(IHttpContextAccessor httpContextAccessor) : base()
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			AccountOwnerRequirement requirement)
		{
			try
			{
				var value = (string)_httpContextAccessor.HttpContext.Request.RouteValues["accountId"];

				var result = context.User.FindFirst
				(
					claim =>
					claim.Type == ClaimTypes.NameIdentifier &&
					claim.Value == value.ToString()
				);

				if (result != null)
				{
					context.Succeed(requirement);
				}
			}
			catch (Exception e)
			{
				return Task.CompletedTask;
			}

			return Task.CompletedTask;
		}
	}
}
