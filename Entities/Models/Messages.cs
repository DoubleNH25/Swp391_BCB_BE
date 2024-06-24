namespace Entities.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public string? Message { get; set; }
        public DateTime SendTime { get; set; }

        public User? User { get; set; }
        public ChatRoom? ChatRoom { get; set; }
    }
}
