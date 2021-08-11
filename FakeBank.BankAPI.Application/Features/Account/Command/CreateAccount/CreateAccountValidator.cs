using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Domain.Entities;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(p => p.CustomerID)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.InitialDepAmount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.AccountType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(DoesCustomerIdExist)
                .WithMessage("Customer ID does not exist.");
            RuleFor(e => e)
                .MustAsync(IsValidAccountType)
                .WithMessage("Account Type must be either Savings or Current.");


        }

        private async Task<bool> DoesCustomerIdExist(CreateAccountCommand e, CancellationToken token)
        {
            return await _accountRepository.DoesCustomerIDExist(e.CustomerID);
        }

        private Task<bool> IsValidAccountType(CreateAccountCommand e, CancellationToken token)
        {
            var isValid = AccountTypeData.AccountType.Contains(e.AccountType.ToLower());
            return Task.FromResult(isValid);

        }
    }
}
