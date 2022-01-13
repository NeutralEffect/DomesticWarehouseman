using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Resource
{
	public class ResourceIndexDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CategoryName { get; set; }
		public string ProviderName { get; set; }
	}
}
