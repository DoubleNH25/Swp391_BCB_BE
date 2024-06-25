namespace Entities.Models
{
    public enum TransactionStatus
    {
        Processing = 0,
        PaymentSuccess = 1,
        PaymentFailure = 2,
        Played = 3,
        Reporting = 4,
        ReportResolved = 5
    }

    public partial class Transaction
    {
        public Transaction()
        {
            HistoryTransactions = new HashSet<HistoryTransaction>();
            Reports = new HashSet<Report>();
            Slots = new HashSet<Slot>();
            ScheduledJob = new HashSet<HangfireJob>();
        }

        public int Id { get; set; }
        public int? IdUser { get; set; }
        public DateTime? TimeTrans { get; set; }
        public string? MethodTrans { get; set; }
        public string? TypeTrans { get; set; }
        public decimal? MoneyTrans { get; set; }
        public int? Status { get; set; }
        public DateTime? DeadLine { get; set; }

        public virtual Slot? IdSlotNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
        public virtual ICollection<HistoryTransaction> HistoryTransactions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
        public virtual ICollection<HangfireJob> ScheduledJob { get; set; }
    }
}