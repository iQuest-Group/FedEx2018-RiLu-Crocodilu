using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Attendance = new HashSet<Attendance>();
            Resources = new HashSet<Resources>();
        }

        public int LessonId { get; set; }
        public int? ModuleId { get; set; }

        public Module Module { get; set; }
        public Homework Homework { get; set; }
        public Presentation Presentation { get; set; }
        public ICollection<Attendance> Attendance { get; set; }
        public ICollection<Resources> Resources { get; set; }
    }
}
