using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Index("TransactionId", Name = "IX_Slot_TransactionId")]
    [Index("IdPost", Name = "IX_Slot_idPost")]
    public partial class Slot
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idSlot")]
        public int? IdSlot { get; set; }
        [Column("idPost")]
        public int? IdPost { get; set; }
        public int? TransactionId { get; set; }

        [ForeignKey("IdSlot")]
        [InverseProperty("Slots")]
        public virtual SlotPost? IdSlotNavigation { get; set; }
        [ForeignKey("TransactionId")]
        [InverseProperty("Slots")]
        public virtual Transaction? Transaction { get; set; }
    }
}
