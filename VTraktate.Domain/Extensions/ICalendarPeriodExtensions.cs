using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VTraktate.Domain.Interfaces;namespace VTraktate.Domain.Extensions
{
    public static class ICalendarPeriodExtensions
    {
        public static bool IsChronologyValid(this ICalendarPeriod @this)
        {
            if (@this.EndDate == null)
                return true;
            return @this.StartDate <= @this.EndDate.Value;
        }
    }
}
