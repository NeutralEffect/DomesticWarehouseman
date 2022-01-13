using DomesticWarehousemanWebApi.DTO.Item;
using DomesticWarehousemanWebApi.Security.Attributes;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api")]
	[ApiController]
	[Authorize]
	public class ItemsController : Controller
	{
		private readonly IItemsService _itemsService;

		public ItemsController(IItemsService itemsService) : base() 
		{
			_itemsService = itemsService;
		}

		[HttpGet("storages/{storageId}/items/index", Name = nameof(IndexItems))]
		[StorageMemberAuthorize]
		public ActionResult<IEnumerable<ItemIndexDto>> IndexItems(
			[FromRoute(Name = "storageId")] int storageId)
		{
			var result = _itemsService.IndexItems(storageId);

			return Ok(result);
		}

		[HttpGet("storages/{storageId}/items/expiring", Name = nameof(IndexExpiringItems))]
		[StorageMemberAuthorize]
		public ActionResult<IEnumerable<ItemIndexDto>> IndexExpiringItems(
			[FromRoute(Name = "storageId")] int storageId,
			[FromQuery(Name = "timeSpan")] TimeSpan timeSpan)
		{
			var result = _itemsService.IndexExpiringItems(storageId, timeSpan);

			return Ok(result);
		}

		[HttpGet("storages/{storageId}/items/{itemId}", Name = nameof(GetItemDetails))]
		[StorageMemberAuthorize]
		public ActionResult<ItemDetailsDto> GetItemDetails(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "itemId")] int itemId)
		{
			var result = _itemsService.GetItemDetails(storageId, itemId);

			return Ok(result);
		}

		[HttpPost("storages/{storageId}/items/new")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> CreateItem(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] ItemCreateDto dto)
		{
			var itemId = await _itemsService.CreateItem(dto, storageId);

			return CreatedAtRoute
			(
				nameof(GetItemDetails), 
				new
				{ 
					StorageId = storageId, 
					ItemId = itemId
				}, 
				null
			);
		}

		[HttpPut("storages/{storageId}/items/{itemId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> UpdateItem(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "itemId")] int itemId,
			[FromBody] ItemUpdateDto dto)
		{
			await _itemsService.UpdateItem(dto, storageId, itemId);

			return NoContent();
		}

		[HttpDelete("storages/{storageId}/items/{itemId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> DeleteItem(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "itemId")] int itemId,
			[FromBody] ItemDeleteDto dto)
		{
			await _itemsService.DeleteItem(dto, storageId, itemId);

			return NoContent();
		}
	}

}
