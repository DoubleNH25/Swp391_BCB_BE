namespace Entities.Models
{
    public partial class HistoryTransaction
    {
        public HistoryTransaction()
        {
            UserRatings = new HashSet<UserRating>();
        }

        public int Id { get; set; }
        public int? IdUserFrom { get; set; }
        public int? IdUserTo { get; set; }
        public int? IdTransaction { get; set; }
        public decimal? MoneyTrans { get; set; }
        public bool? Status { get; set; }
        public DateTime? Deadline { get; set; }

        public virtual Transaction? IdTransactionNavigation { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
