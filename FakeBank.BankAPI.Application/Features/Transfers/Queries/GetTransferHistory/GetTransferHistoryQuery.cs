using MediatR;
using System;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class GetTransferHistoryQuery : IRequest<GetTransferHistoryQueryResponse>
    {
        /// <summary>
        /// Account number used to get the transaction history
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Query with a payment start date
        /// </summary>
        public DateTimeOffset PaymentStartDate { get; set; }
        /// <summary>
        /// Query with a payment end date
        /// </summary>
        public DateTimeOffset PaymentEndDate { get; set; }
        /// <summary>
        /// Page to be returned to the view
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Size of the data list returns
        /// </summary>
        public int Size { get; set; }
    }
}
