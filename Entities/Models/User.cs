using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public enum Skills
    {
        Beginer = 1,
        Junior = 2,
        Middle = 3,
        Expert = 4,
        Athlete = 5
    }
    [Index("UserRole", Name = "IX_Users_userRole")]
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
            SubscriptionUserSubs = new HashSet<Subscription>();
            SubscriptionUsers = new HashSet<Subscription>();
            Transactions = new HashSet<Transaction>();
            UserRatings = new HashSet<UserRating>();
            VerifyTokens = new HashSet<VerifyToken>();
            Wallets = new HashSet<Wallet>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("userName")]
        public string UserName { get; set; } = null!;
        [Column("userPassword")]
        public string? UserPassword { get; set; }
        [Column("fullName")]
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        [Column("phoneNumber")]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column("imgUrl")]
        public string? ImgUrl { get; set; }
        [Column("totalRate")]
        public int? TotalRate { get; set; }
        [Column("rate")]
        public double? Rate { get; set; }
        [Column("userRole")]
        public int? UserRole { get; set; }
        [Column("deviceToken")]
        public string? DeviceToken { get; set; }
        public string? Email { get; set; }
        public string? PlayingArea { get; set; }
        public int PlayingLevel { get; set; }
        public string? PlayingWay { get; set; }
        public string? SortProfile { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsBanFromLogin { get; set; }
        public string? LogingingDevice { get; set; }
        public bool IsAndroidDevice { get; set; }
        [Column("isCheckPolicy")]
        public bool? IsCheckPolicy { get; set; }

        [ForeignKey("UserRole")]
        [InverseProperty("Users")]
        public virtual Role? UserRoleNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("IdUserToNavigation")]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty("UserSub")]
        public virtual ICollection<Subscription> SubscriptionUserSubs { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Subscription> SubscriptionUsers { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [InverseProperty("IdUserRatedNavigation")]
        public virtual ICollection<UserRating> UserRatings { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<VerifyToken> VerifyTokens { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
