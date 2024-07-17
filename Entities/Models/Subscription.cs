using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("Subscription")]
    [Index("UserId", Name = "IX_Subscription_userId")]
    [Index("UserSubId", Name = "IX_Subscription_userSubId")]
    public partial class Subscription
    {
        [Key]
        public int Id { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("userSubId")]
        public int UserSubId { get; set; }
        public bool IsSubcription { get; set; }
        public bool IsBanded { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("SubscriptionUsers")]
        public virtual User User { get; set; } = null!;
        [ForeignKey("UserSubId")]
        [InverseProperty("SubscriptionUserSubs")]
        public virtual User UserSub { get; set; } = null!;
    }
}
