using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.DTO.Account
{
	public class AccountSignInDtoValidator : AbstractValidator<AccountSignInDto>
	{
		public AccountSignInDtoValidator(): base()
		{
			RuleFor(o => o.Email)
				.NotEmpty()
				.EmailAddress()
				.MaximumLength(1000);

			RuleFor(o => o.Password)
				.NotEmpty()
				.MaximumLength(1000);
		}
	}
}
