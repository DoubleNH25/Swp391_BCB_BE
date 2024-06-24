using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserSubId { get; set; }
        public bool IsSubcription { get; set; }
        public bool IsBanded { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual User UserSub { get; set; } = null!;
    }
}
