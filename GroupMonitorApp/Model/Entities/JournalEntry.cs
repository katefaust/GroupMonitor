using System;
using System.ComponentModel;

namespace GroupMonitorApp.Model.Entities
{
    public class JournalEntry
    {
        public virtual int Id { get; protected set; }
        public virtual Student Stud { get; set; }
        public virtual int SubjNumber { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual SchedulesEntry DaySchedules { get; set; }
        [DefaultValue(StudentPresence.Present)]
        public virtual StudentPresence FirstHour { get; set; }
        [DefaultValue(StudentPresence.Present)]
        public virtual StudentPresence SecondHour { get; set; }
    }
}
