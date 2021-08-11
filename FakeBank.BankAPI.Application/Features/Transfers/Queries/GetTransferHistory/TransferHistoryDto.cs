using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class TransferHistoryDto
    {
        /// <summary>
        /// Generated payment reference for the transaction history
        /// </summary>
        public string PaymentReference { get; set; }
        /// <summary>
        /// Payment date of the transaction
        /// </summary>
        public DateTimeOffset PaymentDate { get; set; }
        /// <summary>
        /// Transaction amount initiated
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// Transaction remarks passed during transaction intiation
        /// </summary>
        public string TransactionRemarks { get; set; }
        /// <summary>
        /// beneficaiary account which got the credited amount
        /// </summary>
        public string BeneficiaryAccountNumber { get; set; }
        /// <summary>
        /// beneficiary account name
        /// </summary>
        public string BeneficiaryAccountName { get; set; }
        /// <summary>
        /// shows if the transaction was a debit or credit
        /// </summary>
        public string DebitCreditIndicator { get; set; }
        /// <summary>
        /// shows the account balance of the customer befor debit has occured
        /// </summary>
        public decimal AccountBalanceBeforeDebit { get; set; }
    }
}
