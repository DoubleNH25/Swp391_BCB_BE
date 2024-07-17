using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("UserRating")]
    [Index("IdTransaction", Name = "IX_UserRating_idTransaction")]
    [Index("IdUserRated", Name = "IX_UserRating_idUserRated")]
    public partial class UserRating
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idUserRate")]
        public int? IdUserRate { get; set; }
        [Column("idUserRated")]
        public int? IdUserRated { get; set; }
        [Column("time", TypeName = "datetime")]
        public DateTime? Time { get; set; }
        [Column("levelSkill")]
        public double? LevelSkill { get; set; }
        [Column("friendly")]
        public double? Friendly { get; set; }
        [Column("trusted")]
        public double? Trusted { get; set; }
        [Column("helpful")]
        public double? Helpful { get; set; }
        public string? Content { get; set; }
        [Column("idTransaction")]
        public int? IdTransaction { get; set; }

        [ForeignKey("IdTransaction")]
        [InverseProperty("UserRatings")]
        public virtual HistoryTransaction? IdTransactionNavigation { get; set; }
        [ForeignKey("IdUserRated")]
        [InverseProperty("UserRatings")]
        public virtual User? IdUserRatedNavigation { get; set; }
    }
}
