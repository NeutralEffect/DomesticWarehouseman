using DomesticWarehousemanWebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : Controller
	{
		private readonly DomesticWarehousemanDbContext context;

		public TestController(DomesticWarehousemanDbContext context): base()
		{
			this.context = context;
		}

		[HttpGet("dbconnection")]
		public ActionResult<String> TestDbConnection()
		{
			return Ok
			(
				context.Roles.Select
				(
					role => new 
					{
						Identifier = role.Id + 1000,
						DisplayName = role.Name
					}
				)
			);
		}

		[HttpGet("authorization")]
		[Authorize]
		public ActionResult<string> TestAuthorizaion()
		{
			return Ok
			(
				"Authorized"
			);
		}
	}
}
