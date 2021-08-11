using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance;
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

namespace FakeBank.BankAPI.UnitTests.Account.Queries
{
    public class GetAccountBalanceQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccountRepository> _mockAccountRepository;

        public GetAccountBalanceQueryHandlerTest()
        {
            _mockAccountRepository = AccountRepositoryMock.GetAccountRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetAccountBalanceTest()
        {
            var handler = new GetAccountBalanceQueryHandler(_mapper, _mockAccountRepository.Object, new NullLogger<GetAccountBalanceQueryHandler>());

            var result = await handler.Handle(new GetAccountBalanceQuery { AccountNumber = "2050000003" }, CancellationToken.None);

            result.ShouldBeOfType<GetAccountBalanceQueryResponse>();

            result.Success.ShouldBe(true);
        }
    }
}
