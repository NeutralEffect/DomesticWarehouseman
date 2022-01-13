using DomesticWarehousemanWebApi.DTO.Resource;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api/resources")]
	[ApiController]
	[Authorize]
	public class ResourcesController : Controller
	{
		private readonly IResourcesService _resourcesService;

		public ResourcesController(IResourcesService resourcesService) : base()
		{
			_resourcesService = resourcesService;
		}

		[HttpPost("new")]
		public async Task<ActionResult> CreateResource(
			[FromBody] ResourceCreateDto dto)
		{
			throw new NotImplementedException();
		}

		[HttpGet("{resourceId}")]
		public ActionResult<ResourceDetailsDto> GetResourceDetails(
		[FromRoute(Name = "resourceId")] int resourceId)
		{
			throw new NotImplementedException();
		}

		[HttpGet("index")]
		public ActionResult<IEnumerable<ResourceIndexDto>> IndexResources()
		{
			throw new NotImplementedException();
		}

		[HttpPut("{resourceId}")]
		public async Task<ActionResult> UpdateResource(
			[FromRoute(Name = "resourceId")] int resourceId)
		{
			throw new NotImplementedException();
		}

		[HttpPost("merge")]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> MergeResources(
			[FromBody] ResourceMergeDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
