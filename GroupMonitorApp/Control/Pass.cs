using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMonitorApp.Control
{
    public class Pass
    {
        public int ValidHours { get; set; }
        public int NotValidHours { get; set; }
        public int ValidLessons { get; set; }
        public int NotValidLessons { get; set; }
        public Pass()
        {
            NotValidHours = 0;
            NotValidLessons = 0;
            ValidHours = 0;
            ValidLessons = 0;
        }
    }
}
