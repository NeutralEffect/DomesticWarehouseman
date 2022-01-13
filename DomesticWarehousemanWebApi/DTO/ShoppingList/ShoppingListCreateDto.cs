using DomesticWarehousemanWebApi.DTO.ShoppingListEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingList
{
	public class ShoppingListCreateDto
	{
		public string Name { get; set; }
		public int? CategoryId { get; set; }
		public IEnumerable<ShoppingListEntryCreateDto> Entries { get; set; }
	}
}
