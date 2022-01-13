using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingListEntry
{
	public class ShoppingListEntryDetailsDto
	{
		public int ResourceId { get; set; }
		public int Amount { get; set; }
		public bool Checked { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
