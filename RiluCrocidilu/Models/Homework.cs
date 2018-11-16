using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Homework
    {
        public int HomeworkId { get; set; }
        public int? LessonId { get; set; }
        public string Comments { get; set; }
        public bool? IsDone { get; set; }

        public Lesson Lesson { get; set; }
    }
}
