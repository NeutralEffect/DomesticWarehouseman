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
	public class StorageItemsController : Controller
	{
		public StorageItemsController(): base() { }

		// GET: api/storages/{storageId}/items
		[HttpGet("storages/{storageId}/items")]
		public async Task<ActionResult> GetItemsInStorage(
			[FromRoute(Name = "storageId")] int storageId)
		{
			return Ok();
		}

		// GET: api/storages/{storageId}/items/{itemId}
		[HttpGet("storages/{storageId}/items/{itemId}")]
		public async Task<ActionResult> GetItem(
			[FromRoute(Name = "storageID")] int storageId,
			[FromRoute(Name = "itemId")] int itemId)
		{
			return Ok();
		}

		// PUT: api/storages/{storageId}/items/{itemId}
		[HttpPut("storages/{storageId}/items/{itemId}")]
		public async Task<ActionResult> UpdateItem(
			[FromRoute(Name = "storageID")] int storageId,
			[FromRoute(Name = "itemId")] int itemId)
		{
			return Ok();
		}

		// GET: api/storages/{storageId}/items
		[HttpPost("storages/{storageId}/items")]
		public async Task<ActionResult> CreateItem(
			[FromRoute(Name = "storageID")] int storageId)
		{
			return Ok();
		}

		// GET: api/storages/{storageId}/items/{itemId}
		[HttpDelete("storages/{storageId}/items/{itemId}")]
		public async Task<ActionResult> DeleteItem(
			[FromRoute(Name = "storageID")] int storageId,
			[FromRoute(Name = "itemId")] int itemId)
		{
			return Ok();
		}
	}
}
