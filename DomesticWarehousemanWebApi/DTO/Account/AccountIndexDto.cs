﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountIndexDto
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public bool SystemAdministrator { get; set; }
	}
}
