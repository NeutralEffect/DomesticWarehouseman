﻿using Microsoft.AspNetCore.Authorization;
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
	public class StorageMembersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
