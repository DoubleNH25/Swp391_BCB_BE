using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class JoinedPost
    {
        public int PostId { get; set; }
        public string? PostTitle { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? AvailableSlot { get; set; }
        public string? Status { get; set; }
        public string? AreaName { get; set; }
        public decimal? MoneyPaid { get; set; }
        public int TransacionId { get; set; }
        public string? CoverImage { get; set; }
        public bool CanReport { get; set; }
        public string? ChatRoomUrl { get; set; }
        public int ChatRoomId { get; set; }
        public bool IsCancel { get; set; }
        public List<BookedSlotInfo>? BookedInfos { get; set; }
    }

    public class BookedSlotInfo
    {
        public int CreateSlot { get; set; }
        public int BookedSlot { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
