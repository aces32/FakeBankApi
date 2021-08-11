using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Domain.Entities
{
    public class TransactionHistory
    {
        public Guid TransactionHistoryID { get; set; }
        public string PaymentReference { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionRemarks { get; set; }
        public char DebCredFlag { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public decimal AccountBalanceBeforeDebit { get; set; }
        public int AccountsID { get; set; }
        public Accounts Accounts { get; set; }
    }
}
