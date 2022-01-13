using DomesticWarehousemanWebApi.DTO.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Item
{
	public class ItemDetailsDto
	{
		public int Id { get; set; }
		public ResourceDetailsDto Resource { get; set; }
		public DateTime? ExpirationDate { get; set; }
	}
}
