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
    public class AccountRepositoryMock
    {
        public static Mock<IAccountRepository> GetAccountRepository()
        {
            var accountList = new List<Accounts>
            {
                new Accounts
                {
                    AccountsID = 1,
                    AccountNumber = "2050000001",
                    AccountType = "SAVINGS",
                    ClearedBalance = 10,
                    LedgerBalance = 0,
                    CustomersID = 2
                },
                new Accounts
                {
                    AccountsID = 2,
                    AccountNumber = "2050000003",
                    AccountType = "CURRENT",
                    ClearedBalance = 15,
                    LedgerBalance = 0,
                    CustomersID = 1
                }
            };

            var customerList = new List<Customers>
            {
                new Customers
                {
                    CustomersID = 1,
                    CustomerName = "Ayo Sky",
                    Accounts = accountList
                },
                new Customers
                {
                    CustomersID = 2,
                    CustomerName = "Tola Starr",
                    Accounts = accountList
                }
            };

             

            var mockGasSizeRepository = new Mock<IAccountRepository>();

            mockGasSizeRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(accountList);

            mockGasSizeRepository.Setup(repo => repo.GetAccountInfoByAccountNumber(It.IsAny<string>()))
                .ReturnsAsync((string accountNumber) => 
                {
                    return accountList.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
                });

            mockGasSizeRepository.Setup(repo => repo.DoesAccountExist(It.IsAny<string>()))
                .ReturnsAsync((string accountNumber) =>
                {
                    return accountList.Any(x => x.AccountNumber == accountNumber);
                });       
            
            mockGasSizeRepository.Setup(repo => repo.GetLastAccountID())
                .ReturnsAsync(accountList.Max(x => (int?)x.AccountsID) ?? 0);


            mockGasSizeRepository.Setup(repo => repo.DoesCustomerIDExist(It.IsAny<int>()))
                .ReturnsAsync((int custId) =>
                {
                    var custExist = customerList.Any(x => x.CustomersID == custId);
                    return custExist;
                });

            mockGasSizeRepository.Setup(repo => repo.AddAsync(It.IsAny<Accounts>())).ReturnsAsync(
                (Accounts accounts) =>
                {
                    accountList.Add(accounts);
                    return accounts;
                });

            return mockGasSizeRepository;
        }
    }
}
