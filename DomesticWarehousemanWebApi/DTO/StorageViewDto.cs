using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO
{
	public class StorageViewDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int UsersCount { get; set; }
		public int ItemsCount { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
