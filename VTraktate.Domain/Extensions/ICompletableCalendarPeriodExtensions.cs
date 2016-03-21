using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain.Extensions
{
    public static class ICompletableCalendarPeriodExtensions
    {
        public static bool IsCompleted(this ICompletableCalendarPeriod @this)
        {
            return @this.CompletionDate.HasValue;
        }
        public static bool IsOverDue(this ICompletableCalendarPeriod @this)
        {
            if (@this.IsCompleted())
                return false;
            return @this.EndDate.GetValueOrDefault() < DateTime.Now;
        }

        public static bool IsCompletedPastDue(this ICompletableCalendarPeriod @this)
        {
            if (!@this.IsCompleted())
                return false;
            return @this.EndDate.GetValueOrDefault() < @this.CompletionDate.GetValueOrDefault();
        }
    }
}
