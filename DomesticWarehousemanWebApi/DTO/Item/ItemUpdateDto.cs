using System;

namespace DomesticWarehousemanWebApi.DTO.Item
{
	public class ItemUpdateDto
	{
		public int? NewResourceId { get; set; }
		public DateTime? NewExpirationDate { get; set; }
	}
}