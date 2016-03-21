using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;


namespace VTraktate.Domain.Extensions
{
    public static class ExtendICalendarPeriod
    {
        public static Expression<Func<T, bool>> Current<T>()
            where T : class, ICalendarPeriod
        {
            var currentDate = DateTime.Today;
            
            return y => (y.StartDate <= currentDate) && (!y.EndDate.HasValue && y.EndDate.Value >= currentDate);
        }
    }
}
