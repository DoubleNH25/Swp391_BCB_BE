using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Keyless]
    [Table("Wishlist")]
    [Index("IdPost", Name = "IX_Wishlist_idPost")]
    [Index("UserId", Name = "IX_Wishlist_idUser")]
    public partial class Wishlist
    {
        public int? UserId { get; set; }
        [Column("idPost")]
        public int? IdPost { get; set; }

        [ForeignKey("IdPost")]
        public virtual Post? IdPostNavigation { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
