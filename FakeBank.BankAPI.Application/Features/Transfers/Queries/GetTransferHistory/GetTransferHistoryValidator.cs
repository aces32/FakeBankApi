using FakeBank.BankAPI.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class GetTransferHistoryValidator : AbstractValidator<GetTransferHistoryQuery>
    {
        private readonly IAccountRepository _accountRepository;

        public GetTransferHistoryValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(p => p.AccountNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PaymentStartDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Size)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Page)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PaymentEndDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(DoesAccountNumberExist)
                .WithMessage("Account Number specified does not exist.");

            RuleFor(e => e)
                .MustAsync(DatePassedGreaterValidation)
                .WithMessage("Payment start date cannot be greater than payment end date.");
        }

        private async Task<bool> DoesAccountNumberExist(GetTransferHistoryQuery e, CancellationToken token)
        {
            return await _accountRepository.DoesAccountExist(e.AccountNumber);
        }

        private Task<bool> DatePassedGreaterValidation(GetTransferHistoryQuery e, CancellationToken token)
        {
            var isValid = true;
            if (e.PaymentStartDate > e.PaymentEndDate)
                isValid = false;
            return Task.FromResult(isValid);

        }
    }
}
