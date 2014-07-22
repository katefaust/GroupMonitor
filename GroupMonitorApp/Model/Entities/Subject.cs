using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupMonitorApp.Model.Entities
{
    public class Subject
    {
        public virtual int Id { get; protected set; }
        public virtual String Name { get; set; }
    }
}
