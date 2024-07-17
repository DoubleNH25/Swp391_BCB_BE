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
                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.HistoryTransactions)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("FK_HistoryTransaction_Transactions");
            });

            modelBuilder.Entity<HistoryWallet>(entity =>
            {
                entity.HasOne(d => d.IdWalletNavigation)
                    .WithMany(p => p.HistoryWallets)
                    .HasForeignKey(d => d.IdWallet)
                    .HasConstraintName("FK_HistoryWallet_Wallet");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Notifications");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_Posts_TypePost");

                entity.HasOne(d => d.IdUserToNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdUserTo)
                    .HasConstraintName("FK_Posts_Users");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasOne(d => d.IdSlotNavigation)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.IdSlot)
                    .HasConstraintName("FK_Slots_SlotPost");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Slots_Transactions");
            });

            modelBuilder.Entity<SlotPost>(entity =>
            {
                entity.HasKey(e => e.IdSlot)
                    .HasName("PK__Slot__AC137DE5E4F17BDA");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany(p => p.SlotPosts)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_SlotPost_Posts");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubscriptionUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcribe_Users");

                entity.HasOne(d => d.UserSub)
                    .WithMany(p => p.SubscriptionUserSubs)
                    .HasForeignKey(d => d.UserSubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcribe_Users1");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PhoneNumber).IsFixedLength();

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("FK_UserRating_HistoryTransaction");

                entity.HasOne(d => d.IdUserRatedNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.IdUserRated)
                    .HasConstraintName("FK_UserRating_Users");
            });

            modelBuilder.Entity<VerifyToken>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.VerifyTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tokens_Users");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Wallet_Users");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Wishlist_Posts");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Wishlist_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
