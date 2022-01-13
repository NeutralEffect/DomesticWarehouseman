using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Resource
{
	public class ResourceCreateDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[] Image { get; set; }
		public int? IdProvider { get; set; }
		public int? IdCategory { get; set; }
		public int IdStorage { get; set; }
	}
}
