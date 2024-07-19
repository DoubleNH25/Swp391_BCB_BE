using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;

namespace Services.Interfaces
{
    public interface INotificationServices
    {
        Task<bool> ReadedAll(ReadedNoti info);
        Task<NotiResponseModel> SendNotification(NotificationModel notificationModel);
        Task<NotiResponseModel> SendNotification(int userId, string title, string message, NotificationType type, int referenceInfo);
        Task<NotiResponseModel> SendNotification(List<int> userIds, string title, string message, NotificationType type, int referenceInfo);
        bool TransactionDetailsEmail(Transaction transaction);
    }
}
