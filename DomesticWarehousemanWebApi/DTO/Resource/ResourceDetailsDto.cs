using DomesticWarehousemanWebApi.DTO.Category;
using DomesticWarehousemanWebApi.DTO.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Resource
{
	public class ResourceDetailsDto
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[] Image { get; set; }
		public ProviderDetailsDto Provider { get; set; }
		public CategoryDetailsDto Category { get; set; }
	}
}
