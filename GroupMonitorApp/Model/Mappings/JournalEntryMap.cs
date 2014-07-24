using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using GroupMonitorApp.Model.Entities;

namespace GroupMonitorApp.Model.Mappings
{
    public class JournalEntryMap : ClassMap<JournalEntry>
    {
        public JournalEntryMap()
        {
            Id(x => x.Id);
            References(x => x.Stud).UniqueKey("Entries").Not.Nullable().Not.LazyLoad();
            Map(x => x.Day).UniqueKey("Entries").CustomType("date").Not.Nullable();
            Map(x => x.SubjNumber).UniqueKey("Entries").Not.Nullable();
            References(x => x.DaySchedules).Not.Nullable();
            Map(x => x.FirstHour).Not.Nullable();
            Map(x => x.SecondHour).Not.Nullable();
        }
    }
}
