using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.ResponseObject
{
    public class JoinedPost
    {
        public int PostId { get; set; }
        public string? PostTitle { get; set; }
        public string? AvailableSlot { get; set; }
        public string? Status { get; set; }
        public string? AreaName { get; set; }
        public decimal? MoneyPaid { get; set; }
        public int TransacionId { get; set; }
        public string? CoverImage { get; set; }
        public bool IsCancel { get; set; }
        public List<PostSlot>? BookedInfos { get; set; }
    }

   
}
