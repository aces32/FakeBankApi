using FakeBank.BankAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class GetTransferHistoryQueryResponse : BaseResponse
    {
        public GetTransferHistoryQueryResponse() : base()
        {

        }

        public TransferHistoryListViewModel Data { get; set; }
    }
}
