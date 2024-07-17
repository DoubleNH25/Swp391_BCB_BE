using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("Wallet")]
    [Index("UserId", Name = "IX_Wallet_idUser")]
    public partial class Wallet
    {
        public Wallet()
        {
            HistoryWallets = new HashSet<HistoryWallet>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Column("balance", TypeName = "money")]
        public decimal? Balance { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Wallets")]
        public virtual User? User { get; set; }
        [InverseProperty("IdWalletNavigation")]
        public virtual ICollection<HistoryWallet> HistoryWallets { get; set; }
    }
}
