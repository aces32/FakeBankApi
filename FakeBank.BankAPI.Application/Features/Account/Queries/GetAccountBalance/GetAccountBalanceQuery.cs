using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance
{
    public class GetAccountBalanceQuery : IRequest<GetAccountBalanceQueryResponse>
    {
        /// <summary>
        /// Account Number used to retreive balance
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
