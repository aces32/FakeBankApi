using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers
{
    public class IntraBankTransfersCommand : IRequest<IntraBankTransfersCommandResponse>
    {
        /// <summary>
        /// Unique payment reference to be passed by customer
        /// </summary>
        public string PaymentReference { get; set; }
        /// <summary>
        /// Transaction amount to debit
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// Transaction remarks to be passed by customer
        /// </summary>
        public string TransactionRemarks { get; set; }
        /// <summary>
        /// Account to be debit for transaction
        /// </summary>
        public string AccountNumberToDebit { get; set; }
        /// <summary>
        /// Beneficiary account to be credited
        /// </summary>
        public string BeneficiaryAccountNumber { get; set; }
        /// <summary>
        /// Beneficiary account name
        /// </summary>
        public string BeneficiaryAccountName { get; set; }
    }
}
