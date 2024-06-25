namespace Entities.ResponseObject
{
    public class UserInformation
    {
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? Token { get; set; }
        public bool IsNewUser { get; set; }
        public string? PlayingArea { get; set; }
        public int PlayingLevel { get; set; }
        public string? PlayingWay { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SortProfile { get; set; }
        public int Id { get; set; }
        public decimal? Balance { get; set; }
        public string? Role { get; set; }
        public bool? isPolicy { get; set; }
    }
}
