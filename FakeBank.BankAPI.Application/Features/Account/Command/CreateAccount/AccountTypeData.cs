using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount
{
    public static class AccountTypeData
    {
        public static readonly string[] AccountType = { "savings", "current" };
    }
}
