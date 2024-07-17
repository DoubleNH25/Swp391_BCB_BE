using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class PostOptional
    {
        public int? IdPost {  get; set; }
        public string? Title {  get; set; }
        public string? ContentPost { get; set; }
        public string? ImgUrlPost { get; set; }
        public string? AddressSlot { get; set; }
        public string? Days { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? QuantitySlot { get; set; }
        public string? FullName { get; set; }
        public string? UserImgUrl { get; set; }
        public string? HighlightUrl { get; set; }
        public decimal? Price { get; set; }
        public int? UserId { get; set; }
    }
}
