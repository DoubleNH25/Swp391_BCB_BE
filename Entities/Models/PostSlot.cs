using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Index("IdType", Name = "IX_Posts_idType")]
    [Index("IdUserTo", Name = "IX_Posts_idUserTo")]
    public partial class Post
    {
        public Post()
        {
            SlotPosts = new HashSet<SlotPost>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idType")]
        public int? IdType { get; set; }
        public bool IsDeleted { get; set; }
        public int? IdUserTo { get; set; }
        public string? Title { get; set; }
        public int TotalViewer { get; set; }
        [Column("addressSlot")]
        public string? AddressSlot { get; set; }
        [Column("categorySlot")]
        [StringLength(50)]
        public string? CategorySlot { get; set; }
        [Column("contentPost")]
        public string? ContentPost { get; set; }
        [Column("imgUrl")]
        public string? ImgUrl { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        public DateTime SavedDate { get; set; }
        public string? ImageUrls { get; set; }

        [ForeignKey("IdType")]
        [InverseProperty("Posts")]
        public virtual TypePost? IdTypeNavigation { get; set; }
        [ForeignKey("IdUserTo")]
        [InverseProperty("Posts")]
        public virtual User? IdUserToNavigation { get; set; }
        [InverseProperty("IdPostNavigation")]
        public virtual ICollection<SlotPost> SlotPosts { get; set; }
    }
}
