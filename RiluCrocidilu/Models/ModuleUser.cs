using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class ModuleUser
    {
        public ModuleUser()
        {
            Attendance = new HashSet<Attendance>();
            ChatMessageToUser = new HashSet<ChatMessage>();
            ChatMessageUser = new HashSet<ChatMessage>();
            LoggedInUsers = new HashSet<LoggedInUsers>();
            PrivateMessageToUser = new HashSet<PrivateMessage>();
            PrivateMessageUser = new HashSet<PrivateMessage>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AspNetUserId { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }

        public AspNetUsers AspNetUser { get; set; }
        public ICollection<Attendance> Attendance { get; set; }
        public ICollection<ChatMessage> ChatMessageToUser { get; set; }
        public ICollection<ChatMessage> ChatMessageUser { get; set; }
        public ICollection<LoggedInUsers> LoggedInUsers { get; set; }
        public ICollection<PrivateMessage> PrivateMessageToUser { get; set; }
        public ICollection<PrivateMessage> PrivateMessageUser { get; set; }
    }
}
