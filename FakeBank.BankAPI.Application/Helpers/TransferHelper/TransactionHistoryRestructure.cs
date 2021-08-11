using FakeBank.BankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Helpers.TransferHelper
{
    public static class TransactionHistoryRestructure
    {
        public static TransactionHistory TransactionHistoryToSubmit(Accounts accounts, char debCredFlag, TransactionHistory transaction)
        {
            transaction.PaymentDate = DateTimeOffset.UtcNow;
            transaction.DebCredFlag = debCredFlag;
            transaction.AccountsID = accounts.AccountsID;
            transaction.AccountBalanceBeforeDebit = accounts.ClearedBalance;

            return transaction;
        }
    }
}
