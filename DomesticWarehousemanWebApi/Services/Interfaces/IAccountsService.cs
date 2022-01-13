using DomesticWarehousemanWebApi.DTO.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services.Interfaces
{
	public interface IAccountsService
	{
		public Task<String> GenerateJwtToken(String email, String password);
		public Task<AccountDetailsDto> GetAccountDetails(int accountId);
		public IEnumerable<AccountIndexDto> IndexAccounts();
		public Task<int> RegisterAccount(AccountRegisterDto dto);
		public Task UpdateAccount(AccountUpdateDto dto, int accountId);
	}
}
