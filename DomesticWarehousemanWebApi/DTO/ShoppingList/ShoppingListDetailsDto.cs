using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.DTO.Category;
using DomesticWarehousemanWebApi.DTO.ShoppingListEntry;
using DomesticWarehousemanWebApi.DTO.StorageMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingList
{
	public class ShoppingListDetailsDto
	{
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public IEnumerable<ShoppingListEntryDetailsDto> Entries { get; set; }
		public AccountIndexDto Creator { get; set; }
		public AccountIndexDto Owner { get; set; }
		public CategoryIndexDto Category { get; set; }
	}
}
