using DomesticWarehousemanWebApi.DTO.ShoppingListEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingList
{
	public class ShoppingListUpdateDto
	{
		public string NewName { get; set; }
		public IEnumerable<int> RemovedEntries { get; set; }
		public IEnumerable<ShoppingListEntryCreateDto> NewEntries { get; set; }
		public int? NewOwnerId { get; set; }
		public int? NewCategoryId { get; set; }
	}
}
