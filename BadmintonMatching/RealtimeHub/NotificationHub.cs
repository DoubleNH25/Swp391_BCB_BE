using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;

namespace BadmintonMatching.RealtimeHub
{
    public class NotificationHub : Hub
    {
        private static readonly IDictionary<string, int> _connectionIds = new Dictionary<string, int>();
        private readonly IUserServices _userServices;

        public NotificationHub(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task ReceiveNoti(int user_id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user_id.ToString());
            _connectionIds[Context.ConnectionId] = user_id;

            var fullName = _userServices.GetSelfProfile(user_id).FullName;

            await Clients.Group(user_id.ToString()).SendAsync("ReceiveMessage", "Bot notification", $"{fullName} đã sẵn sàng nhận thông báo");
        }
    }
}
