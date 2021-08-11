using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountCommandResponse>
    {
        /// <summary>
        /// Customer ID to be specified
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Initial Deposit Amount
        /// </summary>
        public decimal InitialDepAmount { get; set; }
        /// <summary>
        /// Account type can be savings or currents
        /// </summary>
        public string AccountType { get; set; } 
    }
}
