using System;
using System.Collections.Generic;

namespace RiluCrocidilu.Models
{
    public partial class Module
    {
        public Module()
        {
            Lesson = new HashSet<Lesson>();
        }

        public int ModuleId { get; set; }
        public string Details { get; set; }

        public Schedule Schedule { get; set; }
        public ICollection<Lesson> Lesson { get; set; }
    }
}
