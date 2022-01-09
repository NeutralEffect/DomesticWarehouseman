﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Security.Requirements
{
	public class StorageEditorRequirement : IAuthorizationRequirement
	{
		public string RoleName { get; }

		public StorageEditorRequirement()
		{
			this.RoleName = Constants.StorageEditorMembership;
		}
	}
}
