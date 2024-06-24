namespace Entities.Models
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            Users = new HashSet<UserChatRoom>();
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CoverImage { get; set; }
        public DateTime? UpdateTime { get; set; }

        public ICollection<UserChatRoom> Users { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}