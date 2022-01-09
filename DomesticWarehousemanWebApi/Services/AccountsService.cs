using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomesticWarehousemanWebApi.Repos.Interface;
using DomesticWarehousemanWebApi.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using DomesticWarehousemanWebApi.Security;

namespace DomesticWarehousemanWebApi.Services
{
	public interface IAccountsService
	{
		public Task<String> GenerateJwtToken(AccountSignInDto asiDto);
		public Task<int> RegisterAccount(AccountRegisterDto arDto);
	}

	public class AccountsService : IAccountsService
	{
		private IPasswordHasher<Account> _passwordHasher;
		private AuthenticationSettings _authenticationSettings;
		private IAccountRepo _accountRepo;

		public AccountsService(
			IAccountRepo accountRepo,
			IPasswordHasher<Account> passwordHasher,
			AuthenticationSettings authenticationSettings)
		{
			_accountRepo = accountRepo;
			_passwordHasher = passwordHasher;
			_authenticationSettings = authenticationSettings;
		}

		public async Task<int> RegisterAccount(AccountRegisterDto arDto)
		{
			if ((await _accountRepo.GetFirstAsync(account => account.Email == arDto.Email)) != null)
			{
				throw new BadRequestException("This email is already in use");
			}

			var account = new Account()
			{
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				Email = arDto.Email,
				DisplayName = arDto.Email
			};

			account.PasswordHash = _passwordHasher.HashPassword(account, arDto.Password);

			await _accountRepo.Add(account);
			await _accountRepo.SaveChanges();

			return account.Id;
		}

		public async Task<String> GenerateJwtToken(AccountSignInDto asiDto)
		{
			var account = await _accountRepo.GetAccountWithMemberships(asiDto.Email);

			if (account is null)
			{
				var message =
					"Invalid " +
					nameof(asiDto.Email).ToLower() +
					" or " +
					nameof(asiDto.Password).ToLower();
				throw new BadRequestException(message);
			}

			var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, asiDto.Password);

			if (result == PasswordVerificationResult.Failed)
			{
				var message =
					"Invalid " +
					nameof(asiDto.Email).ToLower() +
					" or " +
					nameof(asiDto.Password).ToLower();
				throw new BadRequestException(message);
			}

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email.ToString()),
				new Claim(ClaimTypes.Name, account.DisplayName.ToString()),
				new Claim
				(
					ClaimTypes.Role, account.SystemAdministrator ? 
					Constants.AdministratorRole :
					Constants.UserRole
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
	}
}
