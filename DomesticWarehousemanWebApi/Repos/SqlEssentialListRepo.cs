using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.Repos.Base;
using DomesticWarehousemanWebApi.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Repos
{
	public class SqlEssentialListRepo : RepoBase<EssentialList>, IEssentialListRepo
	{
		public SqlEssentialListRepo(DomesticWarehousemanDbContext context) : base(context)
		{ }
	}
}
