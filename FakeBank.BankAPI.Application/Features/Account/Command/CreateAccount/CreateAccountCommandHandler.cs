using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Helpers.AccountHelper;
using FakeBank.BankAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<CreateAccountCommandHandler> _logger;

        public CreateAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository,
            ILogger<CreateAccountCommandHandler> logger)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        /// <summary>
        /// An handler to create a new bank account for a customer, this is used by AccountController with the aid of Mediatr
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateAccountCommandResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var createAccountCommandResponse = new CreateAccountCommandResponse();

                var validator = new CreateAccountValidator(_accountRepository);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (validationResult.Errors.Count > 0)
                {
                    createAccountCommandResponse.Success = false;
                    createAccountCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        createAccountCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }

                if (createAccountCommandResponse.Success)
                {
                    var account = _mapper.Map<Accounts>(request);
                    // Get initial AccountID to Generate a new accountNumber
                    var lastAccountId = await _accountRepository.GetLastAccountID();
                    // Generate Account number
                    account.AccountNumber = lastAccountId.GenerateAccountNumber();
                    // Create account number
                    account = await _accountRepository.AddAsync(account);

                    createAccountCommandResponse.Message = "Account Added Successfully";
                    createAccountCommandResponse.Data = _mapper.Map<CreatedAccountViewModel>(account);
                }

                return createAccountCommandResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"System Error at CreateAccountCommandHandler {nameof(Handle)}");
                throw;
            }
        }
    }
}
