using DomesticWarehousemanWebApi.DTO.ShoppingList;
using DomesticWarehousemanWebApi.Security.Attributes;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api")]
	[ApiController]
	[Authorize]
	public class ShoppingListsController : Controller
	{
		private readonly IShoppingListsService _shoppingListsService;

		public ShoppingListsController(IShoppingListsService shoppingListsService): base()
		{
			_shoppingListsService = shoppingListsService;
		}

		[HttpPost("storages/{storageId}/shoppinglists/new")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> CreateShoppingList(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] ShoppingListCreateDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpPost("storages/{storageId}/shoppinglists/fromessential")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> CreateShoppingListFromEssentials(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] ShoppingListCreateFromEssentialsDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpGet("storages/{storageId}/shoppinglists/{listId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> DeleteShoppingList(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "listId")] int listId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("storages/{storageId}/shoppinglists/{listId}", Name = nameof(GetShoppingListDetails))]
		[StorageMemberAuthorize("Viewer")]
		public async Task<ActionResult<ShoppingListDetailsDto>> GetShoppingListDetails(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "listId")] int listId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("storages/{storageId}/shoppinglists/index")]
		[StorageMemberAuthorize("Viewer")]
		public async Task<ActionResult<IEnumerable<ShoppingListIndexDto>>> IndexShoppingLists(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpPut("storages/{storageId}/shoppinglists/{listId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> UpdateShoppingList(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "listId")] int listId,
			[FromBody] ShoppingListUpdateDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
