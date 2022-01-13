using DomesticWarehousemanWebApi.DTO.Storage;
using DomesticWarehousemanWebApi.Security.Attributes;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class StoragesController : Controller
	{
		private readonly IStoragesService _storagesService;

		public StoragesController(IStoragesService storagesService): base()
		{
			_storagesService = storagesService;
		}

		[HttpPost("create")]
		[Authorize(Roles = "User")]
		public async Task<ActionResult> CreateStorage(
			[FromBody] StorageCreateDto dto)
		{
			int accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			int storageId = await _storagesService.CreateStorage(dto, accountId);

			return CreatedAtRoute
			(
				nameof(GetStorageDetails),
				new { StorageId = storageId },
				null
			);
		}

		[HttpDelete("{storageId}")]
		[StorageMemberAuthorize("Owner")]
		public async Task<ActionResult> DeleteStorage(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("{storageId}", Name = nameof(GetStorageDetails))]
		[StorageMemberAuthorize]
		public async Task<ActionResult<StorageDetailsDto>> GetStorageDetails(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("index", Name = nameof(IndexStoragesForAccount))]
		[Authorize(Roles = "User")]
		public async Task<ActionResult<StorageDetailsDto>> IndexStoragesForAccount()
		{
			int accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			throw new NotImplementedException();
		}

		[HttpPut("{storageId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> UpdateStorage(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] StorageUpdateDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
