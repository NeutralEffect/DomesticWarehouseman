using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomesticWarehousemanWebApi.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DomesticWarehousemanWebApi.Services
{
	public class AccountsService : IAccountsService
	{
		private readonly DomesticWarehousemanDbContext _dbContext;
		private readonly IPasswordHasher<Account> _passwordHasher;
		private readonly AuthenticationSettings _authenticationSettings;

		public AccountsService(
			DomesticWarehousemanDbContext dbContext,
			IPasswordHasher<Account> passwordHasher,
			AuthenticationSettings authenticationSettings)
		{
			_dbContext = dbContext;
			_passwordHasher = passwordHasher;
			_authenticationSettings = authenticationSettings;
		}

		public async Task<String> GenerateJwtToken(String email, String password)
		{
			var account = await _dbContext.Accounts
				.Include(account => account.StorageMembers)
				.ThenInclude(storageMember => storageMember.IdRoleNavigation)
				.FirstOrDefaultAsync(account => account.Email == email);

			if (account is null)
			{
				throw new BadRequestException("Invalid email or password");
			}

			var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);

			if (result == PasswordVerificationResult.Failed)
			{
				throw new BadRequestException("Invalid email or password");
			}

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email.ToString()),
				new Claim(ClaimTypes.Name, account.DisplayName.ToString()),
				new Claim
				(
					ClaimTypes.Role, account.SystemAdministrator ? 
					Constants.SystemAdministratorRole :
					Constants.SystemUserRole
				)
			};

			// Adding claim for each storage membership
			foreach (var it in account.StorageMembers)
			{
				claims.Add
				(
					new Claim
					(
						it.IdRoleNavigation.Name,
						it.IdStorage.ToString()
					)
				);
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddSeconds(_authenticationSettings.JwtExpireSeconds);

			var jwtToken = new JwtSecurityToken
			(
				_authenticationSettings.JwtIssuer,
				_authenticationSettings.JwtIssuer,
				claims,
				expires: expires,
				signingCredentials: credentials
			);

			var tokenHandler = new JwtSecurityTokenHandler();

			return tokenHandler.WriteToken(jwtToken);
		}

		public async Task<AccountDetailsDto> GetAccountDetails(int accountId)
		{
			var account = await _dbContext.Accounts
				.FirstOrDefaultAsync(account => account.Id == accountId);

			if (account == null)
			{
				throw new NotFoundException("Account does not exist");
			}

			return new AccountDetailsDto()
			{
				Id = account.Id,
				DisplayName = account.DisplayName,
				CreationDate = account.CreatedOn,
				Email = account.Email
			};
		}

		public IEnumerable<AccountIndexDto> IndexAccounts()
		{
			var result = _dbContext.Accounts
				.Select
				(
					account =>
					new AccountIndexDto()
					{
						Id = account.Id,
						DisplayName = account.DisplayName,
						SystemAdministrator = account.SystemAdministrator
					}
				);

			return result;
		}

		/// <summary>
		/// Creates new account and saves it in the database
		/// </summary>
		/// <param name="email">Email for created account</param>
		/// <param name="password">Password for created account</param>
		/// <param name="confirmPassword">Password confirmation, must match password</param>
		/// <returns>Id of created account</returns>
		public async Task<int> RegisterAccount(AccountRegisterDto dto)
		{
			if ((await _dbContext.Accounts.FirstOrDefaultAsync(account => account.Email == dto.Email)) != null)
			{
				throw new BadRequestException("This email is already in use");
			}

			var account = new Account()
			{
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				Email = dto.Email,
				DisplayName = dto.Email
			};

			account.PasswordHash = _passwordHasher.HashPassword(account, dto.Password);

			var entry = _dbContext.Accounts.Add(account);
			await _dbContext.SaveChangesAsync();

			return account.Id;
		}

		public async Task UpdateAccount(AccountUpdateDto dto, int accountId)
		{
			var account = await _dbContext.Accounts
				.FirstOrDefaultAsync(account => account.Id == accountId);

			if (account == null)
			{
				throw new NotFoundException("Account not found");
			}

			bool changed = false;

			if (dto.NewEmail != null)
			{
				changed = true;
				account.Email = dto.NewEmail;
			}

			if (dto.NewDisplayName != null)
			{
				changed = true;
				account.DisplayName = dto.NewDisplayName;
			}

			if (changed)
			{
				account.UpdatedOn = DateTime.Now;
				_dbContext.Entry(account).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
