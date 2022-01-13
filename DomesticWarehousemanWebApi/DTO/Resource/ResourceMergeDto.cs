using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Resource
{
	public class ResourceMergeDto
	{
		public int ResourceParentId { get; set; }
		public int ResourceChildId { get; set; }
	}
}
