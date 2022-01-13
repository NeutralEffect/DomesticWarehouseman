using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.Attributes
{
	public class StorageMemberAuthorizeAttribute : AuthorizeAttribute
	{
		private const string POLICY_PREFIX = "Storage";

		private string _storageRole;

		public StorageMemberAuthorizeAttribute(string storageRole = Constants.StorageViewerRole): base()
		{
			StorageRole = storageRole;
		}

		public string StorageRole 
		{
			get
			{
				return _storageRole;
			}
			set
			{
				_storageRole = value;
				Policy = $"{POLICY_PREFIX}{value}";
			}
		}
	}
}
