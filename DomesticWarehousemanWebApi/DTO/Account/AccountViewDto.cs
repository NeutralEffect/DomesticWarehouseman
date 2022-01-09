using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountViewDto
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public DateTime CreationDate { get; set; }
		public bool Administrator { get; set; }
	}
}
