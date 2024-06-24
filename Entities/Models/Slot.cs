namespace Entities.Models
{
    public partial class Slot
    {
        public Slot()
        {
        }

        public int Id { get; set; }
        public int? SlotNumber { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public string? ContentSlot { get; set; }
        public int? IdPost { get; set; }
        public int? IdUser { get; set; }
        public int? TransactionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Post? IdPostNavigation { get; set; }
        public virtual Transaction? Transaction { get; set; }
        public virtual User? User { get; set; }
    }
}
