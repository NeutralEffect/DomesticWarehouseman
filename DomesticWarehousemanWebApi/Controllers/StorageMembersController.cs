using DomesticWarehousemanWebApi.DTO.StorageMember;
using DomesticWarehousemanWebApi.Security.Attributes;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api")]
	[ApiController]
	[Authorize]
	public class StorageMembersController : Controller
	{
		private readonly IStorageMembersService _storageMembersService;

		public StorageMembersController(IStorageMembersService storageMembersService) : base()
		{
			_storageMembersService = storageMembersService;
		}

		[HttpGet("storage/{storageId}/members/index")]
		[StorageMemberAuthorize]
		public async Task<ActionResult<StorageMemberIndexDto>> IndexStorageMembers(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("storage/{storageId}/members/{memberId}", Name = nameof(GetStorageMemberDetails))]
		[StorageMemberAuthorize]
		public async Task<ActionResult<StorageMemberDetailsDto>> GetStorageMemberDetails(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "memberId")] int memberId)
		{
			throw new NotImplementedException();
		}

		[HttpPost("storage/{storageId}/members/new")]
		[StorageMemberAuthorize("Owner")]
		public async Task<ActionResult> CreateStorageMember(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] StorageMemberCreateDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpPut("storage/{storageId}/members/{memberId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> UpdateStorageMember(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "memberId")] int memberId,
			[FromBody] StorageMemberUpdateDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("storage/{storageId}/members/{memberId}")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> DeleteStorageMember(
			[FromRoute(Name = "storageId")] int storageId,
			[FromRoute(Name = "memberId")] int memberId)
		{
			throw new NotImplementedException();
		}
	}
}
