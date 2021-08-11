using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Helpers.AccountHelper
{
    public static class AccountNumberGenerator
    {
        public static string GenerateAccountNumber(this int initialAccountID)
        {
            var newAccountId = initialAccountID + 1;
            return $"3{newAccountId.ToString().PadLeft(9, '0')}";

        }
    }
}
