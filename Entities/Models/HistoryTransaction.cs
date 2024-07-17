using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("HistoryTransaction")]
    [Index("IdTransaction", Name = "IX_HistoryTransaction_idTransaction")]
    public partial class HistoryTransaction
    {
        public HistoryTransaction()
        {
            UserRatings = new HashSet<UserRating>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idTransaction")]
        public int? IdTransaction { get; set; }
        [Column("moneyTrans", TypeName = "money")]
        public decimal? MoneyTrans { get; set; }
        public bool? Status { get; set; }
        public int? UserId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime? Date { get; set; }

        [ForeignKey("IdTransaction")]
        [InverseProperty("HistoryTransactions")]
        public virtual Transaction? IdTransactionNavigation { get; set; }
        [InverseProperty("IdTransactionNavigation")]
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
