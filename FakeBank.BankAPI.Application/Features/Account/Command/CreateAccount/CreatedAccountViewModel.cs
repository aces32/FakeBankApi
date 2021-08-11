using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreatedAccountViewModel
    {
        /// <summary>
        /// Account Number Created
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account type for the created account (can be savings or current)
        /// </summary>
        public string AccountType { get; set; }
        /// <summary>
        /// Account balance for the created account
        /// </summary>
        public decimal AccountBalance { get; set; }
    }
}
