using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Helpers.TransferHelper
{
    public static class TransactionPaymentReference
    {
        public static string GeneratePaymentReference(decimal amount, string paymentReference, string accountNumber)
        {
            string uniqueIdentifier = $"REF:{accountNumber}" +
                    $"{amount.ToString().Replace(",", "").Replace(".", "").PadRight(20, '0')}{paymentReference.ToUpper().Trim().PadRight(30, '0')}{DateTimeOffset.Now:yyyyMMdd}";
                    uniqueIdentifier = uniqueIdentifier.Trim().Length < 75 ? uniqueIdentifier.Trim().PadRight(75, '0') : uniqueIdentifier.Trim().Substring(0, 75);
            return uniqueIdentifier;
        }
    }
}
