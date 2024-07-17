using System.ComponentModel.DataAnnotations;

namespace Entities.RequestObject
{
    public class NoticesRequest
    {
        [Required]
        public string? Message { get; set; }
    }
}
