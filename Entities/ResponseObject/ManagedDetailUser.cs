namespace Entities.ResponseObject
{
    public class ManagedDetailUser
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public bool? IsBanded { get; set; }
        public List<UserManagedPost> Posts { get; set; }
    }

    public class UserManagedPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PostTime { get; set; }
        public int NumOfReport { get; set; }
    }
}
