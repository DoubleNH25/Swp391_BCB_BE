using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("VerifyToken")]
    [Index("UserId", Name = "IX_VerifyToken_UserId")]
    public partial class VerifyToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("VerifyTokens")]
        public virtual User User { get; set; } = null!;
    }
}
