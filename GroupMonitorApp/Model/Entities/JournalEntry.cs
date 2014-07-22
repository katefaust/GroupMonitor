using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMonitorApp.Model.Entities
{
    public class JournalEntry
    {
        public virtual int Id { get; protected set; }
        public virtual Student Stud { get; set; }
        public virtual int SubjNumber { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual SchedulesEntry DaySchedules { get; set; }
        public virtual int Absent { get; set; }
        public virtual bool Valid { get; set; }
    }
}
