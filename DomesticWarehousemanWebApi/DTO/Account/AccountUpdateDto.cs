using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountUpdateDto
	{
		public string NewEmail { get; set; }
		public string NewDisplayName { get; set; }
	}
}
