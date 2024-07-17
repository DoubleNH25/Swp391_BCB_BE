using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Intefaces
{
    public interface IRepositoryManager
    {
        IHistoryTransactionRepository HistoryTransaction { get; }
        IHistoryWalletRepository HistoryWallet { get; }
        IPostRepository Post { get; }
        IRoleRepository Role { get; }
        ISlotRepository Slot { get; }
        ISubscriptionRepository Subscription { get; }
        ITransactionRepository Transaction { get; }
        ITypePostRepository TypePost { get; }
        IUserRatingRepository UserRating { get; }
        IUserRepository User { get; }
        IWalletRepository Wallet { get; }
        IWishlistRepository Wishlist { get; }
        IVerifyTokenRepository VerifyToken { get; }
        ICommentRepository Comment { get; }
        INotificationRepository Notification { get; }
        ISlotPostRepository SlotPost { get; }
        Task SaveAsync();
    }
}
