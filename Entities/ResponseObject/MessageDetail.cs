namespace Entities.ResponseObject
{
    public class MessageDetail
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string SendTime { get; set; }
        public string SendUserName { get; set; }
    }
}
