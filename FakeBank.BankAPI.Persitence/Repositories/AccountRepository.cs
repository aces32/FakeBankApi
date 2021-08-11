using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Persitence.Repositories
{
    public class AccountRepository : BaseRepository<Accounts>, IAccountRepository
    {
        public AccountRepository(FakeBankDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> DoesCustomerIDExist(int customerID)
        {
            var matches = _dbContext.Customers.Any(e => e.CustomersID == customerID);
            return Task.FromResult(matches);
        }

        public Task<bool> DoesAccountExist(string accountNo)
        {
            var matches = _dbContext.Accounts.Any(e => e.AccountNumber == accountNo);
            return Task.FromResult(matches);
        }

        public Task<int> GetLastAccountID()
        {
            var LastAccountId = _dbContext.Accounts.Max(e => (int?)e.AccountsID) ?? 0;
            return Task.FromResult(LastAccountId);
        }

        public Task<Accounts> GetAccountInfoByAccountNumber(string accountNumber)
        {
            var accountDetails = _dbContext.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
            return Task.FromResult(accountDetails);
        }
    }
}
