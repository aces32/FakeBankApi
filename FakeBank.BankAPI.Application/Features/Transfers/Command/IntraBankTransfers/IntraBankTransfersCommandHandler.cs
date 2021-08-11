using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Helpers.TransferHelper;
using FakeBank.BankAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers
{
    public class IntraBankTransfersCommandHandler : IRequestHandler<IntraBankTransfersCommand, IntraBankTransfersCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transactionRepository;
        private readonly ILogger<IntraBankTransfersCommandHandler> _logger;

        public IntraBankTransfersCommandHandler(IMapper mapper, IAccountRepository accountRepository,
            ITransferRepository transactionRepository,
            ILogger<IntraBankTransfersCommandHandler> logger)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        /// <summary>
        /// Transfer between accounts Handler used by TransferController with the aid of Mediatr
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IntraBankTransfersCommandResponse> Handle(IntraBankTransfersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var intraBankTransfersCommandResponse = new IntraBankTransfersCommandResponse();

                var validator = new IntraBankTransfersValidator(_accountRepository, _transactionRepository);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (validationResult.Errors.Count > 0)
                {
                    intraBankTransfersCommandResponse.Success = false;
                    intraBankTransfersCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        intraBankTransfersCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }

                if (intraBankTransfersCommandResponse.Success)
                {
                    // Get account Balance
                    var debitAccountDetails = await _accountRepository.GetAccountInfoByAccountNumber(request.AccountNumberToDebit);
                    var creditAccountDetails = await _accountRepository.GetAccountInfoByAccountNumber(request.BeneficiaryAccountNumber);

                    // Confirm Balance is Sufficient For Debit
                    if (debitAccountDetails.ClearedBalance < request.TransactionAmount)
                    {
                        intraBankTransfersCommandResponse.Success = false;
                        intraBankTransfersCommandResponse.ValidationErrors = new List<string>()
                        { $"You do not have sufficient funds in your account - Balance = {debitAccountDetails.ClearedBalance}" };
                        return intraBankTransfersCommandResponse;
                    }
                    else
                    {
                        // Generate Payment Reference
                        request.PaymentReference = TransactionPaymentReference.GeneratePaymentReference(request.TransactionAmount, 
                            request.PaymentReference, request.AccountNumberToDebit);

                        var debitTransTobeRecorded = TransactionHistoryRestructure.TransactionHistoryToSubmit(debitAccountDetails,
                                        'D', _mapper.Map<TransactionHistory>(request));

                        // Calculate Debit balance
                        debitAccountDetails.ClearedBalance -= request.TransactionAmount;

                        // Update account with new debit balance
                        await _accountRepository.UpdateAsync(debitAccountDetails);
                        _logger.LogInformation($"Account debit has occured for {request.AccountNumberToDebit} - Payment Ref {request.PaymentReference}");

                        // Insert Debit Transaction
                        var debitResponse = await _transactionRepository.AddAsync(debitTransTobeRecorded);
                        _logger.LogInformation($"Transaction debit has been submitted for {request.AccountNumberToDebit} - Payment Ref {request.PaymentReference}");


                        var creditTransTobeRecorded = TransactionHistoryRestructure.TransactionHistoryToSubmit(creditAccountDetails,
                                        'C', _mapper.Map<TransactionHistory>(request));

                        // Calculate Credit balance
                        creditAccountDetails.ClearedBalance += request.TransactionAmount;

                        // Update account with new Credited balance
                        await _accountRepository.UpdateAsync(creditAccountDetails);
                        _logger.LogInformation($"Account Credit has occured for {request.BeneficiaryAccountNumber} - Payment Ref {request.PaymentReference}");

                        // Insert Credit Transaction
                        await _transactionRepository.AddAsync(creditTransTobeRecorded);
                        _logger.LogInformation($"Transaction Credit has been submitted for {request.BeneficiaryAccountNumber} - Payment Ref {request.PaymentReference}");

                        // Return Transfer information to customer
                        intraBankTransfersCommandResponse.Message = "Transaction SuccessFul";
                        intraBankTransfersCommandResponse.Data = _mapper.Map<IntraBankTransfersViewModel>(debitResponse);
                    }

                }

                return intraBankTransfersCommandResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"System Error at IntraBankTransfersCommandHandler {nameof(Handle)}");
                throw;
            }
        }
    }
}
