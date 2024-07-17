using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum NotificationType
    {
        Post = 0,
        User = 1,
    }

    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? About { get; set; }
        public int? ReferenceInfo { get; set; }
        public DateTime NotiDate { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notifications")]
        public virtual User User { get; set; } = null!;
    }
}
