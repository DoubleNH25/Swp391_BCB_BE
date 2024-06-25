using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum WithdrawStatus
    {

        Seccesss,
        Watting,
        Denied,
    }


    public class WithdrawDetail
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AcceptDate { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string BankNumber { get; set; }

    }
}
