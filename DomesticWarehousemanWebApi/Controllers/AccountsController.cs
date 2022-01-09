using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.Repos.Interface;
using DomesticWarehousemanWebApi.Security;
using DomesticWarehousemanWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class AccountsController : Controller
	{
		private readonly IAccountsService _accountsService;
		private readonly IAccountRepo _accountRepo;

		public AccountsController(IAccountsService accountsService, IAccountRepo accountRepo) : base()
		{
			_accountsService = accountsService;
			_accountRepo = accountRepo;
		}

		[HttpGet("{id}", Name = nameof(ViewAccount))]
		[Authorize(Roles = Constants.AdministratorRole)]
		public async Task<ActionResult<AccountViewDto>> ViewAccount(
			[FromRoute] int id)
		{
			var result = await _accountRepo
				.GetFirstAsync(account => account.Id == id);

			if (result is null)
			{
				return NotFound();
			}

			return Ok
			(
				new AccountViewDto()
				{
					Id = result.Id,
					CreationDate = result.CreatedOn,
					DisplayName = result.DisplayName,
					Administrator = result.SystemAdministrator,
					Email = result.Email
				}
			);
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<ActionResult> RegisterAccount(
			[FromBody] AccountRegisterDto arDto)
		{
			var id = await _accountsService.RegisterAccount(arDto);

			return CreatedAtRoute(nameof(ViewAccount), new { id }, null);
		}

		[HttpPost("signin")]
		[AllowAnonymous]
		public async Task<ActionResult> SignIn(
			[FromBody] AccountSignInDto asiDto)
		{
			string token = await _accountsService.GenerateJwtToken(asiDto);
			return Ok(token);
		}

		[HttpGet]
		[Authorize(Roles = Constants.AdministratorRole)]
		public ActionResult<IEnumerable<AccountIndexDto>> IndexAccounts()
		{
			var result = _accountRepo
				.Get(account => true)
				.Select
				(
					account =>
					new AccountIndexDto()
					{
						DisplayName = account.DisplayName,
						CreationDate = account.CreatedOn,
						SystemAdministrator = account.SystemAdministrator,
					}
				);

			return Ok(result);
		}
	}
}
