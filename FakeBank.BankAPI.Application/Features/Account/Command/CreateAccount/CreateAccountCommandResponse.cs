using FakeBank.BankAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommandResponse : BaseResponse
    {
        public CreateAccountCommandResponse() : base()
        {

        }

        public CreatedAccountViewModel Data { get; set; }
    }
}
