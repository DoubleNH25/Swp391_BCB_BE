using Entities.Models;
using Entities.ResponseObject;

namespace Services.Interfaces
{
    public interface SlotServices
    {
        List<HistoryWalletModel> GetHistory(int user_id);
        decimal UpdateBalance(decimal? changes, int user_id, bool isFromTran);
        Task<Wallet> getWallerByUserId(int userId);
    }
}
