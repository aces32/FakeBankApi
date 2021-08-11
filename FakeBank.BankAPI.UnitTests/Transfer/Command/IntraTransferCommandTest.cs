using AutoMapper;
using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers;
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

namespace FakeBank.BankAPI.UnitTests.Transfer.Command
{
    public class IntraTransferCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransferRepository> _mockTransferRepository;

        public IntraTransferCommandTest()
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
        public async Task Handle_IntraTransfer_AddedTransferRepo()
        {
            var handler = new IntraBankTransfersCommandHandler(_mapper, _mockAccountRepository.Object, _mockTransferRepository.Object, 
                new NullLogger<IntraBankTransfersCommandHandler>());
            var intraBankTransfersCommand = new IntraBankTransfersCommand
            {
                AccountNumberToDebit = "2050000001",
                BeneficiaryAccountName = "Ayo Sky",
                BeneficiaryAccountNumber = "2050000003",
                PaymentReference = "TestintraBankTransfersCommand",
                TransactionAmount = 5,
                TransactionRemarks = "Intra bank transfer testing"
            };
            await handler.Handle(intraBankTransfersCommand, CancellationToken.None);

            var alltransferList = await _mockTransferRepository.Object.ListAllAsync();
            alltransferList.Count.ShouldBe(4);
        }
    }
}
