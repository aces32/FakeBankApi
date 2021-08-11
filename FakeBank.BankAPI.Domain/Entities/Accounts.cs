using FakeBank.BankAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Domain.Entities
{
    public class Accounts : AuditableEntity
    {
        public int AccountsID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType{ get; set; }
        public decimal LedgerBalance { get; set; }
        public decimal ClearedBalance { get; set; }
        public int CustomersID { get; set; }
        public Customers Customers { get; set; }
        public ICollection<TransactionHistory> TransactionHistory { get; set; }
    }
}
