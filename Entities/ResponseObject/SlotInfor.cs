using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class SlotInfor
    {
        public int? IdPost { get; set; }
        public string? Days { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? QuantitySlot { get; set; }
        public decimal? Price { get; set; }
    }
}
