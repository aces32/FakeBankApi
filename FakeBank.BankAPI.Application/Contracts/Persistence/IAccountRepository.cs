using FakeBank.BankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Contracts.Persistence
{
    public interface IAccountRepository : IAsyncRepository<Accounts>
    {
        Task<bool> DoesCustomerIDExist(int customerID);
        Task<bool> DoesAccountExist(string accountNo);
        Task<int> GetLastAccountID();
        Task<Accounts> GetAccountInfoByAccountNumber(string accountNumber);
    }
}
