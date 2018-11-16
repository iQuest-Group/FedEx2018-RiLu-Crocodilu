using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class ChatRoom
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public ChatMessage ChatMessage { get; set; }
        public LoggedInUsers LoggedInUsers { get; set; }
    }
}
