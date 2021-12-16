using DomesticWarehousemanWebApi.Models;
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
		private readonly DomesticWarehousemanDBContext context;

		public TestController(DomesticWarehousemanDBContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public string Get()
		{
			return $"{{\"databaseProvider\": \"{context.Database.ProviderName}\"}}";
		}
	}
}
