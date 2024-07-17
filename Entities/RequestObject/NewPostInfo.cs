using System.Globalization;
using Entities.Models;

namespace Entities.RequestObject
{
    public class NewPostInfo
    {
        public string? LevelSlot { get; set; }
        public string? CategorySlot { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public SlotInfo? SlotInfor { get; set; }
        public string? Description { get; set; }
        public string? HighlightUrl { get; set; }
        public List<string>? ImgUrls { get; set; }

    }

   
}
