using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using GroupMonitorApp.Model.Entities;

namespace GroupMonitorApp.Model.Mappings
{
    class SubjectMap : ClassMap<Subject>
    {
        public SubjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
