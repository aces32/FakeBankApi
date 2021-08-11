using FakeBank.BankAPI.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance
{
    public class GetAccountBalanceValidator : AbstractValidator<GetAccountBalanceQuery>
    {

        public GetAccountBalanceValidator()
        {
            RuleFor(p => p.AccountNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(10).WithMessage("{PropertyName} must be 10 digits.");
        }
    }
}
