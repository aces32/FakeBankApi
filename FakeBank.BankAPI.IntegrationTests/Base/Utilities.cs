using FakeBank.BankAPI.Domain.Entities;
using FakeBank.BankAPI.Persitence;
using System;
using System.Collections.Generic;

namespace FakeBank.BankAPI.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(FakeBankDbContext context)
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

            /// Account in memory
            context.Accounts.Add(accountID1);

            context.Accounts.Add(accountID2);


            /// customers inMemory
            context.Customers.Add(new Customers
            {
                CustomersID = 1,
                CustomerName = "Ayo Sky",
            });

            context.Customers.Add(new Customers
            {
                CustomersID = 2,
                CustomerName = "Tola Starr",
            });

            /// Transactions InMemory
            context.TransactionHistory.Add(new TransactionHistory
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
            });


            context.TransactionHistory.Add(new TransactionHistory
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
            });
            context.SaveChanges();
        }
    }
}