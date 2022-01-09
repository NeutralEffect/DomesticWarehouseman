using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Validators
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
				.Custom
				(
					(email, context) =>
					{
						var emailTaken = dbContext.Accounts.Any(account => account.Email == email);
						if (emailTaken)
						{
							context.AddFailure("Email", "This email is already taken");
						}
					}
				);
		}
	}
}
