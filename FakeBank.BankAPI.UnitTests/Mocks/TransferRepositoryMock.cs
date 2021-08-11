using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.UnitTests.Mocks
{
    public class TransferRepositoryMock
    {
        public static Mock<ITransferRepository> GetTransferRepository()
        {
            var accountID1 = new Accounts
            {
                AccountsID = 1,
                AccountNumber = "2050000001",
                AccountType = "SAVINGS",
                ClearedBalance = 10,
                LedgerBalance = 0,
                CustomersID = 2
            };

            var accountID2 = new Accounts
            {
                AccountsID = 2,
                AccountNumber = "2050000003",
                AccountType = "CURRENT",
                ClearedBalance = 15,
                LedgerBalance = 0,
                CustomersID = 1
            };

            var accountList = new List<Accounts>
            {
                accountID1,
                accountID2
            };

            var transferHistoryList = new List<TransactionHistory>
            {
                new TransactionHistory
                {
                    TransactionHistoryID = new Guid(),
                    BeneficiaryAccountName = "Ayo Sky",
                    BeneficiaryAccountNumber = "2050000003",
                    DebCredFlag = 'C',
                    AccountBalanceBeforeDebit = 15,
                    AccountsID = 1,
                    TransactionAmount = 5,
                    TransactionRemarks = "Integration testing Ayo Sky",
                    PaymentDate = DateTimeOffset.Parse("2021-08-09"),
                    PaymentReference = "REF:205000000320000000000000000000TESTPAYMENTREF000000000000000020210809000",
                    Accounts = accountID1
                }, 
                new TransactionHistory
                {
                    TransactionHistoryID = new Guid(),
                    BeneficiaryAccountName = "Tola Starr",
                    BeneficiaryAccountNumber = "2050000001",
                    DebCredFlag = 'D',
                    AccountBalanceBeforeDebit = 10,
                    AccountsID = 2,
                    TransactionAmount = 5,
                    TransactionRemarks = "Integration testing Ayo Sky",
                    PaymentDate = DateTimeOffset.Parse("2021-08-09"),
                    PaymentReference = "REF:205000000320000000000000000000TESTPAYMENTREF000000000000000020210809000",
                    Accounts = accountID2
                }
            };

            var mockGasSizeRepository = new Mock<ITransferRepository>();

            mockGasSizeRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(transferHistoryList);

            mockGasSizeRepository.Setup(repo => repo.GetTransactionHistoryBasedOnAccountNumber(It.IsAny<string>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((string accountNumber, DateTimeOffset paymentStartDate, DateTimeOffset paymentEndDate, int page, int size ) =>
                {
                    return transferHistoryList.Where(s => (s.PaymentDate >= paymentStartDate && s.PaymentDate <= paymentEndDate)
                                        && s.Accounts.AccountNumber == accountNumber)
                                        .Skip((page - 1) * size).Take(size).ToList();
                });

            mockGasSizeRepository.Setup(repo => repo.DoesPaymentReferenceExist(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string paymentReference, string accountNumber) =>
                {
                    var paymentReferenceExist = transferHistoryList.Any(x => x.PaymentReference == paymentReference && 
                    x.Accounts.AccountNumber == accountNumber);
                    return !paymentReferenceExist;
                });

            mockGasSizeRepository.Setup(repo => repo.AddAsync(It.IsAny<TransactionHistory>())).ReturnsAsync(
                (TransactionHistory transactionHistory) =>
                {
                    transferHistoryList.Add(transactionHistory);
                    return transactionHistory;
                });

            return mockGasSizeRepository;
        }
    }
}
