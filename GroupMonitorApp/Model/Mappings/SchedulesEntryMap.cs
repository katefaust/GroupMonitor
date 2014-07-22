using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using GroupMonitorApp.Model.Entities;

namespace GroupMonitorApp.Model.Mappings
{
     public class SchedulesEntryMap : ClassMap<SchedulesEntry>
    {
         public SchedulesEntryMap()
         {
            Id(x => x.Id);
            Map(x => x.DayOfWeek).UniqueKey("Days").Not.Nullable();
            Map(x => x.WeekType).UniqueKey("Days").Not.Nullable();
            References(x => x.Subject).Not.Nullable();
            Map(x => x.SubjNumber).Not.Nullable().UniqueKey("Days").Not.Nullable();
         }
         
    }
}
