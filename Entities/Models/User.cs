using System.Data;

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
    public partial class User
    {
        public User()
        {
/*            Posts = new HashSet<Post>();
            Transactions = new HashSet<Transaction>();
            UserRatings = new HashSet<UserRating>();
            Wallets = new HashSet<Wallet>();
            Tokens = new HashSet<VerifyToken>();
            Notifications = new HashSet<Notification>();
            Slots = new HashSet<Slot>();*/
            ChatRooms = new HashSet<UserChatRoom>();
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? UserPassword { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? ImgUrl { get; set; }
        public int? TotalRate { get; set; }
        public double? Rate { get; set; }
        public int? UserRole { get; set; }
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
        public bool? IsPolicy { get; set; }

        public virtual Role? UserRoleNavigation { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<VerifyToken> Tokens { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
        public virtual ICollection<UserChatRoom> ChatRooms { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
