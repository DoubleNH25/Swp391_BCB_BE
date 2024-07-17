using System.ComponentModel.DataAnnotations;

namespace Entities.RequestObject
{
    public class SendMessageRequest
    {
        [Required]
        public string? Message { get; set; }
        [Required]
        public int? RoomId { get; set; }
    }
}
