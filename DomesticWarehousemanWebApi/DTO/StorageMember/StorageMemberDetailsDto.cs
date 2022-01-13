using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.StorageMember
{
	public class StorageMemberDetailsDto
	{
		public string DisplayName { get; set; }
		public string Role { get; set; }
		public string IdAccount { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
