using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory;
using FakeBank.BankAPI.Application.Profiles;
using FakeBank.BankAPI.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FakeBank.BankAPI.UnitTests.Transfer.Queries
{
    public class GetTransferHistoryQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransferRepository> _mockTransferRepository;

        public GetTransferHistoryQueryHandlerTest()
        {
            _mockAccountRepository = AccountRepositoryMock.GetAccountRepository();
            _mockTransferRepository = TransferRepositoryMock.GetTransferRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task TransferHistory()
        {
            var handler = new GetTransferHistoryQueryHandler(_mapper, _mockAccountRepository.Object, _mockTransferRepository.Object,
                new NullLogger<GetTransferHistoryQueryHandler>());

            var transferHistoryQuery = new GetTransferHistoryQuery
            {
                AccountNumber = "2050000003",
                PaymentEndDate = DateTimeOffset.Parse("2021-08-10"),
                PaymentStartDate = DateTimeOffset.Parse("2021-08-08"),
                Page = 1,
                Size = 2
            };
            var result = await handler.Handle(transferHistoryQuery, CancellationToken.None);

            result.ShouldBeOfType<GetTransferHistoryQueryResponse>();

            result.Success.ShouldBe(true);
        }
    }
}
