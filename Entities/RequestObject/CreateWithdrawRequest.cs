using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestObject
{
    public class CreateWithdrawRequest
    {
        public int IdUser { get; set; }
        public decimal Money { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string BankNumber { get; set; }
    }
}
