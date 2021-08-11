using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Exceptions;
using FakeBank.BankAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, GetAccountBalanceQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<GetAccountBalanceQueryHandler> _logger;

        public GetAccountBalanceQueryHandler(IMapper mapper, IAccountRepository accountRepository,
            ILogger<GetAccountBalanceQueryHandler> logger)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        /// <summary>
        /// An handler to Retrieve balances for a given account, this is used by AccountController with the aid of Mediatr
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetAccountBalanceQueryResponse> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAccountBalanceCommandResponse = new GetAccountBalanceQueryResponse();

                var validator = new GetAccountBalanceValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (validationResult.Errors.Count > 0)
                {
                    getAccountBalanceCommandResponse.Success = false;
                    getAccountBalanceCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        getAccountBalanceCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }

                if (getAccountBalanceCommandResponse.Success)
                {
                    // Get account Balance
                    var accountDetails = await _accountRepository.GetAccountInfoByAccountNumber(request.AccountNumber);

                    if (accountDetails == null)
                    {
                        throw new NotFoundException(nameof(Accounts), request.AccountNumber);
                    }

                    getAccountBalanceCommandResponse.Message = "Success";
                    getAccountBalanceCommandResponse.Data = _mapper.Map<GetAccountViewModel>(accountDetails);
                }

                return getAccountBalanceCommandResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"System Error at GetAccountBalanceCommandHandler {nameof(Handle)}");
                throw;
            }
        }
    }
}
