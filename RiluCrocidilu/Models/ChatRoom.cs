using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            ChatMessage = new HashSet<ChatMessage>();
            LoggedInUsers = new HashSet<LoggedInUsers>();
        }

        public int RoomId { get; set; }
        public string Name { get; set; }

        public ICollection<ChatMessage> ChatMessage { get; set; }
        public ICollection<LoggedInUsers> LoggedInUsers { get; set; }
    }
}
