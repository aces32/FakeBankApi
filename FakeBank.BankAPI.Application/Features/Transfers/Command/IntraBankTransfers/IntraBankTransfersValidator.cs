using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Helpers.TransferHelper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers
{
    public class IntraBankTransfersValidator : AbstractValidator<IntraBankTransfersCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transferRepository;

        public IntraBankTransfersValidator(IAccountRepository accountRepository,
            ITransferRepository transferRepository)
        {
            _accountRepository = accountRepository;
            _transferRepository = transferRepository;
            RuleFor(p => p.AccountNumberToDebit)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.BeneficiaryAccountName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.BeneficiaryAccountNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PaymentReference)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();


            RuleFor(p => p.TransactionRemarks)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TransactionAmount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0);

            RuleFor(e => e)
                .MustAsync(DoesAccountToDebitExist)
                .WithMessage("Account to debit specified does not exist.");

            RuleFor(e => e)
                .MustAsync(DoesPaymentReferenceExist)
                .WithMessage("Payment Reference Already exist, Please use a new reference .");

            RuleFor(e => e)
                .MustAsync(DoesBeneficiaryAccountExist)
                .WithMessage("Beneficiary account specified does not exist.");

            RuleFor(e => e)
                .MustAsync(SameAccountNumberTransferCheck)
                .WithMessage("You cannot initiate transfer to the same account number.");
        }

        private async Task<bool> DoesAccountToDebitExist(IntraBankTransfersCommand e, CancellationToken token)
        {
            return await _accountRepository.DoesAccountExist(e.AccountNumberToDebit);
        }

        private async Task<bool> DoesBeneficiaryAccountExist(IntraBankTransfersCommand e, CancellationToken token)
        {
            return await _accountRepository.DoesAccountExist(e.BeneficiaryAccountNumber);
        }

        private async Task<bool> DoesPaymentReferenceExist(IntraBankTransfersCommand e, CancellationToken token)
        {
            var generatedReference = TransactionPaymentReference.GeneratePaymentReference(e.TransactionAmount,
                            e.PaymentReference, e.AccountNumberToDebit);
            return await _transferRepository.DoesPaymentReferenceExist(generatedReference, e.AccountNumberToDebit);
        }

        private Task<bool> SameAccountNumberTransferCheck(IntraBankTransfersCommand e, CancellationToken token)
        {
            var isValid = true;
            if (e.AccountNumberToDebit == e.BeneficiaryAccountNumber)
                isValid = false;
            return Task.FromResult(isValid);

        }
    }
}
