using DomesticWarehousemanWebApi.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.RequirementHandlers
{
	public class StorageOwnerRequirementHandler : AuthorizationHandler<StorageOwnerRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public StorageOwnerRequirementHandler(IHttpContextAccessor httpContextAccessor) : base()
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			StorageOwnerRequirement requirement)
		{
			try
			{
				var value = (string)_httpContextAccessor.HttpContext.Request.RouteValues["storageId"];

				var result = context.User.FindFirst
				(
					claim =>
					claim.Type == Constants.StorageOwnerRole &&
					claim.Value == value
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
