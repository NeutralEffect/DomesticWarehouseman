using DomesticWarehousemanWebApi.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Item
{
	public class ItemDeleteDtoValidator : AbstractValidator<ItemDeleteDto>
	{
		public ItemDeleteDtoValidator() : base()
		{
		}
	}
}
