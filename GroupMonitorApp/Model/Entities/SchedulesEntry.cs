using System;

namespace GroupMonitorApp.Model.Entities
{
    public class SchedulesEntry
    {
        public virtual int Id { get; protected set; }
        public virtual DayOfWeek DayOfWeek { get; set; }
        public virtual WeekType WeekType { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual int SubjNumber { get; set; }
    }
}
