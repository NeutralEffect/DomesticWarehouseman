using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Item
{
	public class ItemCreateDto
	{
		public DateTime ExpirationDate { get; set; }
		public int ResourceId { get; set; }
	}
}
