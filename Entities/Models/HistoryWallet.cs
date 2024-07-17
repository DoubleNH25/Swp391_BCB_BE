using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities.Models
{
    public enum HistoryWalletStatus
    {
        Success = 1,
        Fail = 0
    }

    public enum HistoryWalletType
    {
        MoneyIn = 0,
        MoneyOut = 1,
        FromAToB = 2
    }
    [Table("HistoryWallet")]
    [Index("IdWallet", Name = "IX_HistoryWallet_idWallet")]
    public partial class HistoryWallet
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idWallet")]
        public int? IdWallet { get; set; }
        public int? UserId { get; set; }
        [Column("amount")]
        public string? Amount { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("time", TypeName = "datetime")]
        public DateTime? Time { get; set; }
        [Column("type")]
        public string? Type { get; set; }

        [ForeignKey("IdWallet")]
        [InverseProperty("HistoryWallets")]
        public virtual Wallet? IdWalletNavigation { get; set; }
    }
}
