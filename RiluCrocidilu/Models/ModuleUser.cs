using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class ModuleUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AspNetUserId { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }

        public AspNetUsers AspNetUser { get; set; }
        public ChatMessage ChatMessageToUser { get; set; }
        public ChatMessage ChatMessageUser { get; set; }
        public LoggedInUsers LoggedInUsers { get; set; }
        public PrivateMessage PrivateMessageToUser { get; set; }
        public PrivateMessage PrivateMessageUser { get; set; }
    }
}
