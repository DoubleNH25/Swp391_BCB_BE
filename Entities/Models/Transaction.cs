using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Entities.Models
{
    public enum TransactionStatus
    {

        Booked = 0,
        Canceled = 1,
        Complete = 2,
    }

    public enum SlotStatus
    {

        Watting = 0,
        Pending = 1,
        Checkin = 2,
        CheckOut = 3,
    }
    [Index("UserId", Name = "IX_Transactions_idUser")]
    public partial class Transaction
    {
        public Transaction()
        {
            HistoryTransactions = new HashSet<HistoryTransaction>();
            Slots = new HashSet<Slot>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Column("timeTrans", TypeName = "datetime")]
        public DateTime? TimeTrans { get; set; }
        [Column("methodTrans")]
        public string? MethodTrans { get; set; }
        [Column("typeTrans")]
        public string? TypeTrans { get; set; }
        [Column("moneyTrans", TypeName = "money")]
        public decimal? MoneyTrans { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        public DateTime? DeadLine { get; set; }
        [Column("idpost")]
        public int? Idpost { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Transactions")]
        public virtual User? User { get; set; }
        [InverseProperty("IdTransactionNavigation")]
        public virtual ICollection<HistoryTransaction> HistoryTransactions { get; set; }
        [InverseProperty("Transaction")]
        public virtual ICollection<Slot> Slots { get; set; }
    }
}
