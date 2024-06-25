using Entities.Models;
using Entities.RequestObject;

namespace Entities.ResponseObject
{
    public class PostInfomation
    {
        public PostInfomation() { }
        public PostInfomation(Post post)
        {
            Title = post.Title;
            Address = post.AddressSlot;
            PostId = post.Id;
            PostImgUrl = post.ImgUrl;
            SortDescript = post.ContentPost;
            UserId = post.IdUserTo;
            UserImgUrl = post.IdUserToNavigation.ImgUrl;
            UserName = post.IdUserToNavigation.UserName;
            isDelete = post.IsDeleted;
            status = post.Status;

            var finalInfo = new SlotInfo();
            int joinedSlot = 0;
            foreach (var slot in post.SlotsInfo.Split(";"))
            {
                var slotInfo = new SlotInfo(slot);
                var joinSlot = post.Slots
                    .Where(x =>
                    !x.IsDeleted &&
                    x.ContentSlot == slotInfo.StartTime.Value.ToString("dd/MM/yyyy"))
                    .Count();
                if (slotInfo.AvailableSlot - joinSlot >= finalInfo.AvailableSlot)
                {

                    finalInfo = slotInfo;
                    joinedSlot = joinSlot;
                }
            }
            AvailableSlot = finalInfo.AvailableSlot - joinedSlot;
            Time = $"{finalInfo.StartTime.Value.ToString("dd/MM/yyyy")}: {finalInfo.StartTime.Value.ToString("HH:mm")} - {finalInfo.EndTime.Value.ToString("HH:mm")}";
        }
        //public List<AllSlot>? AllSlotInfo { get; set; }
        public string? Title { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? SortDescript { get; set; }
        public string? Time { get; set; }
        public int? AvailableSlot { get; set; }
        public string? PostImgUrl { get; set; }
        public string? UserImgUrl { get; set; }
        public string? Address { get; set; }
        public bool? isDelete { get; set; }
        public bool? status { get; set; }
        //public class AllSlot
        //{
        //    public int? NumberSlot { get; set; }
        //    public string? UserName { get; set;}
        //    public string? PhoneNumber {  get; set; }
        //    public string? PlayWay {  get; set; }
        //    public string? Gender { get; set; }
        //    public string? Level { get; set; }
        //    public string? TotalRate {  get; set; }
        //}
    }

    public class Room
    {
        public string? PlayDate { get; set; }
        public int Id { get; set; }
    }
}
