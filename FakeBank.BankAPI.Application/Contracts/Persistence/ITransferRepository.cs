using FakeBank.BankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Contracts.Persistence
{
    public interface ITransferRepository : IAsyncRepository<TransactionHistory>
    {
        Task<bool> DoesPaymentReferenceExist(string paymentReference, string accountNumber);
        Task<List<TransactionHistory>> GetTransactionHistoryBasedOnAccountNumber(string accountNumber,
            DateTimeOffset paymentStartDate, DateTimeOffset paymentEndDate, int page, int size);
    }
}
