using DomesticWarehousemanWebApi.DTO.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services.Interfaces
{
	public interface IItemsService
	{
		public Task<int> CreateItem(ItemCreateDto dto, int storageId);
		public ItemDetailsDto GetItemDetails(int storageId, int itemId);
		public IEnumerable<ItemIndexDto> IndexItems(int storageId);
		public IEnumerable<ItemIndexDto> IndexExpiringItems(int storageId, TimeSpan maxTimeToExpiration);
		public Task UpdateItem(ItemUpdateDto dto, int storageId, int itemId);
		public Task DeleteItem(ItemDeleteDto dto, int storageId, int itemId);
	}
}
