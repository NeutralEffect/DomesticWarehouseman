using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Storage
{
	public class StorageUpdateDto
	{
		public string DisplayName { get; set; }
		public int? NewOwner { get; set; }
	}
}
