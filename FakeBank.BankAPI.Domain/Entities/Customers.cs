using FakeBank.BankAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Domain.Entities
{
    public class Customers : AuditableEntity
    {
        public int CustomersID { get; set; }
        public string CustomerName { get; set; }
        public ICollection<Accounts> Accounts { get; set; }
    }
}
