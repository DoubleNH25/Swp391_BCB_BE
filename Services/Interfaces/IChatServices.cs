using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IChatServices
    {
        Task<int> CreateRoom(int admin_id, int reportId);
        //Task<List<ChatInfos>> GetChatRoom(int transactionId);
        Task<List<MessageDetail>> GetRoomDetail(int roomIds, int pageSize, int pageNum);
        Task<List<JoinedChatRoom>> GetRoomOfUser(int user_id);
        Task<bool> JoinRoom(int user_id, int room_id);
        Task<bool> LeaveRoom(int user_id, int room_id);
        Task<User> SendMessage(int user_id, SendMessageRequest info);
        Task JoinOrleaveChat(SendMessageRequest info);
    }
}
