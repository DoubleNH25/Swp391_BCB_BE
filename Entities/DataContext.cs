using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HistoryTransaction> HistoryTransactions { get; set; } = null!;
        public virtual DbSet<HistoryWallet> HistoryWallets { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TypePost> TypePosts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRating> UserRatings { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;
        public virtual DbSet<VerifyToken> VerifyToken { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<ChatRoom> ChatRooms { get; set; } = null!;
        public virtual DbSet<UserChatRoom> UserChatRooms { get; set; } = null!;
        public virtual DbSet<Messages> Messages { get; set; } = null!;
        public virtual DbSet<HangfireJob> ScheduledJob { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<WithdrawDetail> WithdrawDetails { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BadmintonDB1;user id=sa;password=12345;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryTransaction>(entity =>
            {
                entity.ToTable("HistoryTransaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Deadline)
                    .HasColumnType("datetime")
                    .HasColumnName("deadline");

                entity.Property(e => e.IdTransaction).HasColumnName("idTransaction");

                entity.Property(e => e.IdUserFrom).HasColumnName("idUserFrom");

                entity.Property(e => e.IdUserTo).HasColumnName("idUserTo");

                entity.Property(e => e.MoneyTrans)
                    .HasColumnType("money")
                    .HasColumnName("moneyTrans");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.HistoryTransactions)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("FK_HistoryTransaction_Transactions");
            });

            modelBuilder.Entity<HistoryWallet>(entity =>
            {
                entity.ToTable("HistoryWallet");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdWallet).HasColumnName("idWallet");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.IdWalletNavigation)
                    .WithMany(p => p.HistoryWallets)
                    .HasForeignKey(d => d.IdWallet)
                    .HasConstraintName("FK_HistoryWallet_Wallet");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressSlot).HasColumnName("addressSlot");

                entity.Property(e => e.CategorySlot)
                    .HasMaxLength(50)
                    .HasColumnName("categorySlot");

                entity.Property(e => e.ContentPost).HasColumnName("contentPost");

                entity.Property(e => e.IdType).HasColumnName("idType");

                entity.Property(e => e.IdUserTo).HasColumnName("idUserTo");

                entity.Property(e => e.ImgUrl).HasColumnName("imgUrl");

                entity.Property(e => e.LevelSlot)
                    .HasMaxLength(50)
                    .HasColumnName("levelSlot");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_Posts_TypePost");

                entity.HasOne(d => d.IdUserToNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdUserTo)
                    .HasConstraintName("FK_Posts_Users");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.Property(e => e.IdUserFrom).HasColumnName("idUserFrom");

                entity.Property(e => e.IdUserTo).HasColumnName("idUserTo");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TimeReport)
                    .HasColumnType("datetime")
                    .HasColumnName("timeReport");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("FK_Transaction_Reports");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Post_Reports");
            });

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.ToTable("ChatRoom");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<UserChatRoom>(entity =>
            {
                entity.ToTable("UserChatRoom");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatRooms)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ChatRoomUser_User");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_ChatRoomUser_Room");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("Messages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Message_User");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Message_Room");
            });

            modelBuilder.Entity<HangfireJob>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.ScheduledJob)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_HangfireJob_Transaction");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName).HasColumnName("roleName");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Notifications");
            });

            modelBuilder.Entity<VerifyToken>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tokens_Users");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContentSlot).HasColumnName("contentSlot");

                entity.Property(e => e.IdPost).HasColumnName("idPost");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.SlotNumber).HasColumnName("slotNumber");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Slot_Posts");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Slot_Transaction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Slot_User");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Subscription");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserSubId).HasColumnName("userSubId");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcribe_Users");

                entity.HasOne(d => d.UserSub)
                    .WithMany()
                    .HasForeignKey(d => d.UserSubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcribe_Users1");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.MethodTrans).HasColumnName("methodTrans");

                entity.Property(e => e.MoneyTrans)
                    .HasColumnType("money")
                    .HasColumnName("moneyTrans");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TimeTrans)
                    .HasColumnType("datetime")
                    .HasColumnName("timeTrans");

                entity.Property(e => e.TypeTrans).HasColumnName("typeTrans");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<TypePost>(entity =>
            {
                entity.ToTable("TypePost");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypePost1).HasColumnName("typePost");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeviceToken).HasColumnName("deviceToken");

                entity.Property(e => e.FullName).HasColumnName("fullName");

                entity.Property(e => e.ImgUrl).HasColumnName("imgUrl");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsPolicy).HasColumnName("isCheckPolicy");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber")
                    .IsFixedLength();

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.TotalRate).HasColumnName("totalRate");



                entity.Property(e => e.UserName).HasColumnName("userName");

                entity.Property(e => e.UserPassword).HasColumnName("userPassword");

                entity.Property(e => e.UserRole).HasColumnName("userRole");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.ToTable("UserRating");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Friendly).HasColumnName("friendly");

                entity.Property(e => e.Helpful).HasColumnName("helpful");

                entity.Property(e => e.IdTransaction).HasColumnName("idTransaction");

                entity.Property(e => e.IdUserRate).HasColumnName("idUserRate");

                entity.Property(e => e.IdUserRated).HasColumnName("idUserRated");

                entity.Property(e => e.LevelSkill).HasColumnName("levelSkill");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.Property(e => e.Trusted).HasColumnName("trusted");

                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("FK_UserRating_HistoryTransaction");

                entity.HasOne(d => d.IdUserRatedNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.IdUserRated)
                    .HasConstraintName("FK_UserRating_Users");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Wallet_Users");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Wishlist");

                entity.Property(e => e.IdPost).HasColumnName("idPost");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Wishlist_Posts");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Wishlist_Users");
            });


            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Settings");

                entity.Property(e => e.SettingId).HasColumnName("settingId");

                entity.Property(e => e.SettingName)
                    .HasColumnName("settingName");

                entity.Property(e => e.SettingAmount).HasColumnName("settingAmount");
            });

            modelBuilder.Entity<WithdrawDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdUser).HasColumnName("idUserRequest");
                entity.Property(e => e.Money).HasColumnName("price");
                entity.Property(e => e.CreateDate).HasColumnName("createDate");
                entity.Property(e => e.AcceptDate).HasColumnName("acceptDate");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.BankName).HasColumnName("bankName");
                entity.Property(e => e.AccountName).HasColumnName("bankNumber");
                entity.Property(e => e.BankNumber).HasColumnName("AccoutName");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
