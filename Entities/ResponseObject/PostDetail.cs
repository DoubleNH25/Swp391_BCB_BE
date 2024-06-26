using Entities.Models;
using Entities.RequestObject;

namespace Entities.ResponseObject
{
    public class PostDetail
    {
        public PostDetail()
        {

        }
        public PostDetail(Post post)
        {
            AddressSlot = post.AddressSlot;
            CategorySlot = post.CategorySlot;
            ContentPost = post.ContentPost;
            FullName = post.IdUserToNavigation.FullName;
            HightLightImage = post.ImgUrl;
            LevelSlot = post.LevelSlot;
            ImgUrlUser = post.IdUserToNavigation.ImgUrl;
            SortProfile = post.IdUserToNavigation.SortProfile;
            TotalRate = post.IdUserToNavigation.TotalRate;
            UserId = post.IdUserTo.Value;
            Title = post.Title;

            foreach (var slot in post.SlotsInfo.Split(";"))
            {
                if (SlotInfos == null)
                {
                    SlotInfos = new List<SlotInfo>();
                }
                var slotInfo = new SlotInfo(slot);
                var joinSlot = post.Slots
                    .Where(x =>
                    !x.IsDeleted &&
                    x.ContentSlot == slotInfo.StartTime.Value.ToString("dd/MM/yyyy"))
                    .Count();
                slotInfo.AvailableSlot -= joinSlot;

                if (slotInfo.StartTime >= DateTime.UtcNow.AddHours(-7))
                {
                    SlotInfos.Add(slotInfo);
                }
            }
        }

        public string? AddressSlot { get; set; }
        public string? LevelSlot { get; set; }
        public string? CategorySlot { get; set; }
        public string? ContentPost { get; set; }
        public string? HightLightImage { get; set; }
        public List<string>? ImageUrls { get; set; }
        public string? FullName { get; set; }
        public int? TotalRate { get; set; }
        public string? ImgUrlUser { get; set; }
        public string? SortProfile { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public List<SlotInfo> SlotInfos { get; set; }
    }
}
