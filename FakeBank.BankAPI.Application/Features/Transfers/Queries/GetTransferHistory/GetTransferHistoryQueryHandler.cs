using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Exceptions;
using FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers;
using FakeBank.BankAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class GetTransferHistoryQueryHandler : IRequestHandler<GetTransferHistoryQuery, GetTransferHistoryQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transactionRepository;
        private readonly ILogger<GetTransferHistoryQueryHandler> _logger;

        public GetTransferHistoryQueryHandler(IMapper mapper, IAccountRepository accountRepository,
            ITransferRepository transactionRepository,
            ILogger<GetTransferHistoryQueryHandler> logger)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        /// <summary>
        /// Transfer History Handler used by TransferController with the aid of Mediatr
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetTransferHistoryQueryResponse> Handle(GetTransferHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getTransferHistoryQueryResponse = new GetTransferHistoryQueryResponse();

                var validator = new GetTransferHistoryValidator(_accountRepository);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (validationResult.Errors.Count > 0)
                {
                    getTransferHistoryQueryResponse.Success = false;
                    getTransferHistoryQueryResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        getTransferHistoryQueryResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }


                if (getTransferHistoryQueryResponse.Success)
                {
                    var transferHistory = await _transactionRepository.GetTransactionHistoryBasedOnAccountNumber(request.AccountNumber,
                        request.PaymentStartDate, request.PaymentEndDate, request.Page, request.Size);

                    if (transferHistory == null)
                    {
                        throw new NotFoundException(nameof(TransactionHistory), request.AccountNumber);
                    }

                    getTransferHistoryQueryResponse.Data = new TransferHistoryListViewModel
                    {
                        Count = transferHistory.Count,
                        Page = request.Page,
                        Size = request.Size,
                        TransferHistory = _mapper.Map<List<TransferHistoryDto>>(transferHistory)
                    };

                }

                return getTransferHistoryQueryResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"System Error at GetTransferHistoryQueryHandler {nameof(Handle)}");
                throw;
            }
        }
    }
}
