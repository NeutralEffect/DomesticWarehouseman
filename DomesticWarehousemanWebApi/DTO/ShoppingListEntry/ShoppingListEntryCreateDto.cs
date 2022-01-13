using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingListEntry
{
	public class ShoppingListEntryCreateDto
	{
		public int ResourceId { get; set; }
		public int Amount { get; set; }
	}
}
