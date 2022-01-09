using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.Attributes
{
	public class StorageMembershipAuthorizeAttribute : AuthorizeAttribute
	{
		private const string POLICY_PREFIX = "Storage";

		public StorageMembershipAuthorizeAttribute(string membership): base()
		{
			Policy = $"{POLICY_PREFIX}{membership}";
		}
	}
}
