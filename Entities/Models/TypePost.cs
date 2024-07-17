using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    [Table("TypePost")]
    public partial class TypePost
    {
        public TypePost()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("typePost")]
        public string? TypePost1 { get; set; }

        [InverseProperty("IdTypeNavigation")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
