using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int? LessonId { get; set; }
        public int? UserId { get; set; }
        public bool? IsAttending { get; set; }

        public Lesson Lesson { get; set; }
        public ModuleUser User { get; set; }
    }
}
