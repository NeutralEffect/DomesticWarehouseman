using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.Repos.Base;
using DomesticWarehousemanWebApi.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Repos
{
	public class SqlItemRepo : RepoBase<Item>, IItemRepo
	{
		public SqlItemRepo(DomesticWarehousemanDbContext context) : base(context)
		{ }
	}
}
