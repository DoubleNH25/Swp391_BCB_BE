using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("SlotPost")]
    public partial class SlotPost
    {
        public SlotPost()
        {
            Slots = new HashSet<Slot>();
        }

        [Key]
        public int IdSlot { get; set; }
        public int? IdPost { get; set; }
        [StringLength(50)]
        public string? ContextPost { get; set; }
        [StringLength(50)]
        public string? SlotDate { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal SlotPrice { get; set; }

        public int Status { get; set; }

        [ForeignKey("IdPost")]
        [InverseProperty("SlotPosts")]
        public virtual Post? IdPostNavigation { get; set; }
        [InverseProperty("IdSlotNavigation")]
        public virtual ICollection<Slot> Slots { get; set; }
    }
}
