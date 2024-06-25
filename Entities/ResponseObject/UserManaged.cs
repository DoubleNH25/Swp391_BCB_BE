namespace Entities.ResponseObject
{
    public class UserManaged
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? CreateDate { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public string? LastLogin { get; set; }
        public bool IsBanded { get; set; }
    }
}
