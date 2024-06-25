using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class WithdrawDetailResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public decimal Money { get; set; }
        public string CreateDate { get; set; }
        public string AcceptDate { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string BankNumber { get; set; }
    }
}
