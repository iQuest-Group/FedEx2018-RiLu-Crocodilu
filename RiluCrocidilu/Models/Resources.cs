using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Resources
    {
        public int ResourceId { get; set; }
        public int? LessonId { get; set; }
        public string ResourceData { get; set; }

        public Lesson Lesson { get; set; }
    }
}
