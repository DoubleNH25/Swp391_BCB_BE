using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestObject
{
    public class TransactionCreateInfo
    {
        public int? IdUser { get; set; }
        public List<int>? IdSlot { get; set; }
    }
}
