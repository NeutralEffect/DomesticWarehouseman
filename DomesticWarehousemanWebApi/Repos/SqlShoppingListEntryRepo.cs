using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.Repos.Base;
using DomesticWarehousemanWebApi.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Repos
{
	public class SqlShoppingListEntryRepo : RepoBase<ShoppingListEntry>, IShoppingListEntryRepo
	{
		public SqlShoppingListEntryRepo(DomesticWarehousemanDbContext context) : base(context)
		{ }
	}
}
