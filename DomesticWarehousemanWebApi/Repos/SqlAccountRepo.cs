using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.Repos.Base;
using DomesticWarehousemanWebApi.Repos.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace DomesticWarehousemanWebApi.Repos
{
	public class SqlAccountRepo : RepoBase<Account>, IAccountRepo
	{
		public SqlAccountRepo(DomesticWarehousemanDbContext context) : base(context) { }

		public async Task<Account> GetAccountWithMemberships(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Accounts
				.Include(account => account.StorageMembers)
				.ThenInclude(storageMember => storageMember.IdRoleNavigation)
				.FirstOrDefaultAsync(account => account.Id == id, cancellationToken);
		}

		public async Task<Account> GetAccountWithMemberships(string email, CancellationToken cancellationToken = default)
		{
			return await _context.Accounts
				.Include(account => account.StorageMembers)
				.ThenInclude(storageMember => storageMember.IdRoleNavigation)
				.FirstOrDefaultAsync(account => account.Email == email, cancellationToken);
		}
	}
}
