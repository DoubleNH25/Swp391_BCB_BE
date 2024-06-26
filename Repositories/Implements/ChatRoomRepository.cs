using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    public class ChatRoomRepository : RepositoryBase<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(DataContext context) : base(context)
        {

        }
    }
}
