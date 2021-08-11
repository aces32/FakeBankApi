using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory
{
    public class TransferHistoryListViewModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ICollection<TransferHistoryDto> TransferHistory { get; set; }
    }
}
