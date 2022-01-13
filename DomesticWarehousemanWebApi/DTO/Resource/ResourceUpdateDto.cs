using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Resource
{
	public class ResourceUpdateDto
	{
		public int? NewProviderId { get; set; }
		public int? NewCategoryId { get; set; }
		public string NewName { get; set; }
		public byte[] NewImage { get; set; }
		public string NewDescription { get; set; }
	}
}
