using FakeBank.BankAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance
{
    public class GetAccountBalanceQueryResponse : BaseResponse
    {
        public GetAccountBalanceQueryResponse() : base()
        {

        }
        public GetAccountViewModel Data { get; set; }
    }
}
