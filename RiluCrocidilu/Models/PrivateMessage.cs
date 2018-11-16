using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class PrivateMessage
    {
        public int PrivateMessageId { get; set; }
        public int? UserId { get; set; }
        public int? ToUserId { get; set; }
        public string Text { get; set; }
        public DateTime? TimeStamp { get; set; }

        public ModuleUser ToUser { get; set; }
        public ModuleUser User { get; set; }
    }
}
