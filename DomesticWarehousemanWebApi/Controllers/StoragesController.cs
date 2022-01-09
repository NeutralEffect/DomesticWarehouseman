using DomesticWarehousemanWebApi.DTO;
using DomesticWarehousemanWebApi.Repos.Interface;
using DomesticWarehousemanWebApi.Security.Attributes;
using DomesticWarehousemanWebApi.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = Constants.AdministratorRole)]
	public class StoragesController : Controller
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly IStorageRepo _storageRepo;

		public StoragesController(IAuthorizationService authorizationService, IStorageRepo storageRepo): base()
		{
			_authorizationService = authorizationService;
			_storageRepo = storageRepo;
		}

		[HttpGet("{storageId}")]
		[StorageMembershipAuthorize(Constants.StorageViewerMembership)]
		public async Task<ActionResult<StorageViewDto>> ViewStorage(
			[FromRoute(Name = "storageId")] int storageId)
		{
			var storage = await _storageRepo
				.GetFirstAsync(storage => storage.Id == storageId);

			return Ok
			(
				new StorageViewDto()
				{
					Name = storage.DisplayName,
					Id = storage.Id,
					ItemsCount = storage.Items.Count,
					CreationDate = storage.CreatedOn,
					UsersCount = storage.StorageMembers.Count
				}
			);
		}

		// POST: api/storage/create
		[HttpPost("create")]
		[Authorize(Roles = Constants.UserRole)]
		public async Task<ActionResult> CreateStorage()
		{
			return Ok();
		}

		// DELETE: api/storage/{id}
		[HttpDelete("{id}")]
		[StorageMembershipAuthorize(Constants.StorageEditorMembership)]
		public async Task<ActionResult> DeleteStorage(
			[FromRoute(Name = "id")] int id)
		{
			return Ok();
		}
	}
}
