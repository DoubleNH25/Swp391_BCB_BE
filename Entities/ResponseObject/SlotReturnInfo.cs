using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class SlotIncludeTransaction
    {
        public int? TransactionId { get; set; }
        public List<ChatInfos>? ChatInfos { get; set; }
    }
    public class SlotReturnInfo
    {
        public string? Date { get; set; }
        public List<int>? SlotIds { get; set; }
        public string? Message { get; set; }
    }
    public class ChatInfos
    {
        public int? RoomId { get; set; }
        public string? PlayDate { get; set; }
    }
}
