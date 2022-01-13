using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Item
{
	public class ItemIndexDto
	{
		public int Id { get; set; }
		public string ResourceName { get; set; }
		public int ResourceId { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
