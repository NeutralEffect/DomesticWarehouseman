using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.Repos.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Repos.Interface
{
	public interface IAccountRepo : IRepoBase<Account>
	{
		Task<Account> GetAccountWithMemberships(
			int id,
			CancellationToken cancellationToken = default);

		Task<Account> GetAccountWithMemberships(
			string email,
			CancellationToken cancellationToken = default);
	}
}
