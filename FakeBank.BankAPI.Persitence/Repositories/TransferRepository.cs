using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Persitence.Repositories
{
    public class TransferRepository : BaseRepository<TransactionHistory>, ITransferRepository
    {
        public TransferRepository(FakeBankDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> DoesPaymentReferenceExist(string paymentReference, string accountNumber)
        {
            var matches = _dbContext.TransactionHistory.Any(e => e.PaymentReference == paymentReference && e.Accounts.AccountNumber == accountNumber);
            return Task.FromResult(!matches);
        }

        public async Task<List<TransactionHistory>> GetTransactionHistoryBasedOnAccountNumber(string accountNumber, 
            DateTimeOffset paymentStartDate, DateTimeOffset paymentEndDate, int page, int size)
        {          
            return await _dbContext.TransactionHistory.Where(s => (s.PaymentDate >= paymentStartDate && s.PaymentDate <= paymentEndDate) 
                                        && s.Accounts.AccountNumber == accountNumber)
                                        .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}
