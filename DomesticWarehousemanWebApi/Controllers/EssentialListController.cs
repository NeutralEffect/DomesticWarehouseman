using DomesticWarehousemanWebApi.DTO.EssentialList;
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
	public class EssentialListController : Controller
	{
		private readonly IEssentialListService _essentialListService;

		public EssentialListController(IEssentialListService essentialListService) : base()
		{
			_essentialListService = essentialListService;
		}

		[HttpPost("storages/{storageId}/essentiallist")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> CreateEssentialList(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] EssentialListCreateDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("storages/{storageId}/essentiallist")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> DeleteEssentialList(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("storages/{storageId}/essentiallist", Name = nameof(GetEssentialListDetails))]
		[StorageMemberAuthorize]
		public async Task<ActionResult<EssentialListDetailsDto>> GetEssentialListDetails(
			[FromRoute(Name = "storageId")] int storageId)
		{
			throw new NotImplementedException();
		}

		[HttpPut("storages/{storageId}/essentiallist")]
		[StorageMemberAuthorize("Editor")]
		public async Task<ActionResult> UpdateEssentialList(
			[FromRoute(Name = "storageId")] int storageId,
			[FromBody] EssentialListUpdateDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
