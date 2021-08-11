using FakeBank.BankAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers
{
    public class IntraBankTransfersCommandResponse : BaseResponse
    {
        public IntraBankTransfersCommandResponse() : base()
        {

        }

        public IntraBankTransfersViewModel Data { get; set; }
    }
}
