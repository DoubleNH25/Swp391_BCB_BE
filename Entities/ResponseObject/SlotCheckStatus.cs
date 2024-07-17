using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class SlotCheckStatus
    {
        public string? DateSlot { get; set; }
        public List<slotCheck> slots { get; set; }
        public class slotCheck
        {
            public int? transisionId { get; set; }
            public string content { get; set; }
            public int status { get; set; }
        }
    }
}
