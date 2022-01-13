using DomesticWarehousemanWebApi.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.ShoppingList
{
	public class ShoppingListIndexDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public CategoryIndexDto Category { get; set; }
	}
}
