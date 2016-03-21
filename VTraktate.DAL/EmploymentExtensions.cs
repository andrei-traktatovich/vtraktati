using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DAL
{
    public partial class Employment : ICalendarPeriod
    {
        public bool HasCalendarPeriods { get { return this.CalendarPeriods != null && this.CalendarPeriods.Count > 0; } }
    }

}
