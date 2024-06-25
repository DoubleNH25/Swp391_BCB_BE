using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class HistoryWalletModel
    {
        public int Id { get; set; }
        public int? IdWallet { get; set; }
        public int? IdUser { get; set; }
        public string? Amount { get; set; }
        public string? Status { get; set; }
        public string? Time { get; set; }
        public string? Type { get; set; }
        public string? TypeORGN { get; set; }
    }
}
