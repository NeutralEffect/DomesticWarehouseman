using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Controllers
{
	[ApiController]
	[Route("api/accounts")]
	[Authorize]
	public class AccountsController : Controller
	{
		private readonly IAccountsService _accountsService;

		public AccountsController(IAccountsService accountsService) : base()
		{
			_accountsService = accountsService;
		}

		[HttpGet("{accountId}", Name = nameof(GetAccountDetails))]
		[Authorize(Roles = "Administrator", Policy = "AccountOwner")]
		public async Task<ActionResult<AccountDetailsDto>> GetAccountDetails(
			[FromRoute] int accountId)
		{
			var result = await _accountsService.GetAccountDetails(accountId);

			if (result is null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		[HttpGet("index")]
		[Authorize(Roles = "Administrator")]
		public ActionResult<IEnumerable<AccountIndexDto>> IndexAccounts()
		{
			IEnumerable<AccountIndexDto> result = _accountsService
				.IndexAccounts();

			return Ok(result);
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<ActionResult> RegisterAccount(
			[FromBody] AccountRegisterDto dto)
		{
			var accountId = await _accountsService
				.RegisterAccount(dto);

			return CreatedAtRoute
			(
				nameof(GetAccountDetails),
				new { AccountId = accountId },
				null
			);
		}

		[HttpPost("signin")]
		[AllowAnonymous]
		public async Task<ActionResult> SignIn(
			[FromBody] AccountSignInDto dto)
		{
			string token = await _accountsService
				.GenerateJwtToken
				(
					dto.Email,
					dto.Password
				);

			return Ok(token);
		}

		[HttpPut("{accountId}")]
		[Authorize(Policy = "AccountOwner")]
		public async Task<ActionResult> UpdateAccount(
			[FromBody] AccountUpdateDto dto)
		{
			var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			await _accountsService
				.UpdateAccount(dto, accountId);

			return Ok();
		}
	}
}
