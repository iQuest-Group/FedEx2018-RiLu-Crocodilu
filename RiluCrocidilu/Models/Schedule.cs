using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int? ModuleId { get; set; }
        public DateTime? ModuleDay { get; set; }
        public TimeSpan? ModuleHour { get; set; }
        public string ModuleLocation { get; set; }

        public Module Module { get; set; }
    }
}
