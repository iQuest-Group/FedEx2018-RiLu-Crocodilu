using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Presentation
    {
        public int PresentationId { get; set; }
        public int? LessonId { get; set; }
        public byte[] PresentationData { get; set; }
        public string PresentationExtension { get; set; }

        public Lesson Lesson { get; set; }
    }
}
