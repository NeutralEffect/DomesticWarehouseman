using DomesticWarehousemanWebApi.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountRegisterDtoValidator : AbstractValidator<AccountRegisterDto>
	{
		public AccountRegisterDtoValidator(DomesticWarehousemanDbContext dbContext)
		{
			RuleFor(o => o.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(o => o.Password)
				.MinimumLength(6);

			RuleFor(o => o.ConfirmPassword)
				.Equal(o => o.Password);

			RuleFor(o => o.Email)
				.EmailAddress()
				.MaximumLength(1000);
		}
	}
}
