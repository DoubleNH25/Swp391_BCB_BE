namespace Entities.ResponseObject
{
    public class CommentInfos
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public string? UserAvatar { get; set; }
        public int? TotalRate { get; set; }
        public DateTime SavedDate { get; set; }
    }
}
