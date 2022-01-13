using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.Requirements
{
	public class StorageOwnerRequirement : IAuthorizationRequirement
	{
		public string RoleName { get; }

		public StorageOwnerRequirement()
		{
			this.RoleName = Constants.StorageOwnerRole;
		}
	}
}
