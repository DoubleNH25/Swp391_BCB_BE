namespace Entities.ResponseObject
{
    public class NotificationReturn
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? About { get; set; }
        public string? NotiDate { get; set; }
        public bool IsRead { get; set; }
        public int? ReferenceId { get; set; }
    }
}
