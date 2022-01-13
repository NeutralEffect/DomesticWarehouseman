using DomesticWarehousemanWebApi.DTO.ShoppingList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services.Interfaces
{
	public interface IShoppingListsService
	{
		public Task<int> CreateShoppingList(ShoppingListCreateDto dto, int storageId, int accountId);
		public IEnumerable<ShoppingListIndexDto> IndexShoppingLists(int storageId);
		public Task<ShoppingListDetailsDto> GetShoppingListDetails(int storageId, int listId);
		public Task UpdateShoppingList(ShoppingListUpdateDto dto, int storageId, int listId);
		public Task DeleteShoppingList(int storageId, int listId);
	}
}
