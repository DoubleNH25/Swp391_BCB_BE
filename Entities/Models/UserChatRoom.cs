using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserChatRoom
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }

        public User? User { get; set; }
        public ChatRoom? ChatRoom { get; set; }
    }
}
