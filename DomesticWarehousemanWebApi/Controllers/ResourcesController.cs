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
	[Authorize]
	public class ResourcesController : Controller
	{
		public ResourcesController() : base() { }

		// GET: api/resource/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult> GetResorceById(
			[FromRoute(Name = "id")] int id)
		{
			return Ok();
		}

		// PUT: api/resource/{id}
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateResource(
			[FromRoute(Name = "id")] int id)
		{
			return Ok();
		}

		// POST: api/resource
		[HttpPost]
		public async Task<ActionResult> CreateResource()
		{
			return Ok();
		}

		// PUT: api/resource/merge?parentId={1}&childId={2}
		[HttpPut("merge")]
		public async Task<ActionResult> MergeResources(
			[FromQuery(Name = "parentId")] int parentId,
			[FromQuery(Name = "childId")] int childId)
		{
			return Ok();
		}
	}
}
