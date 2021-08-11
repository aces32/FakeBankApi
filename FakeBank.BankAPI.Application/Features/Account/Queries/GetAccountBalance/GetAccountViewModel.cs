using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance
{
    public class GetAccountViewModel
    {
        /// <summary>
        /// Account balance of the specified account number
        /// </summary>
        public string AccountBalance { get; set; }
        /// <summary>
        /// Account type of the specified account number
        /// </summary>
        public string AccountType { get; set; }
    }
}
