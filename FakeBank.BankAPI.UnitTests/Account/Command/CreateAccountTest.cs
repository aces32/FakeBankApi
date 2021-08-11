using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount;
using FakeBank.BankAPI.Application.Profiles;
using FakeBank.BankAPI.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FakeBank.BankAPI.UnitTests.Account.Command
{
    public class CreateAccountTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccountRepository> _mockAccountRepository;

        public CreateAccountTest()
        {
            _mockAccountRepository = AccountRepositoryMock.GetAccountRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidAccount_AddedAccountsRepo()
        {
            var handler = new CreateAccountCommandHandler(_mapper, _mockAccountRepository.Object, new NullLogger<CreateAccountCommandHandler>());

            await handler.Handle(new CreateAccountCommand() { CustomerID = 1, AccountType = "savings", InitialDepAmount = 4 }, CancellationToken.None);

            var allAccountList = await _mockAccountRepository.Object.ListAllAsync();
            allAccountList.Count.ShouldBe(3);
        }
    }
}
