using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class ReportIncomeModel
    {
      public  List<HistoryWalletModel> historyWalletModels { get; set; }
        public decimal Total { get; set; }
    }

}
