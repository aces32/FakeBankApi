using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers
{
    public class IntraBankTransfersViewModel
    {
        /// <summary>
        /// Generated refrence after payment
        /// </summary>
        public string PaymentReference { get; set; }
        /// <summary>
        /// amount that was credited to be beneficiary
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// Remarks specified by the payment initiator
        /// </summary>
        public string TransactionRemarks { get; set; }
        /// <summary>
        /// Date of transaction payment
        /// </summary>
        public DateTimeOffset PaymentDate { get; set; }
    }
}
