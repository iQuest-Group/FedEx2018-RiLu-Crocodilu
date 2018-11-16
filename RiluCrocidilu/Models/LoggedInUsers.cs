using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class LoggedInUsers
    {
        public int LoggedInUserId { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public string ConnectionId { get; set; }

        public ChatRoom Room { get; set; }
        public ModuleUser User { get; set; }
    }
}
