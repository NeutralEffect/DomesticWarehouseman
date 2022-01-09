﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountRegisterDto
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}