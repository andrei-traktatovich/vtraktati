using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DAL
{
    public interface ICalendarPeriod
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }

    public static class ICalendarPeriodExtenstions
    {
        public static T Current<T>(this ICollection<T> source, DateTime? startDate = null, DateTime? endDate = null) where T : class, ICalendarPeriod
        {
            if (source.Count() == 0)
                return null;

            var start = DateOrDefaultDate(startDate);
            var end = DateOrDefaultDate(endDate);
            return source.Where(x => x.StartDate <= start && (!x.EndDate.HasValue || x.EndDate.Value >= end)).FirstOrDefault();
        }
        private static DateTime DateOrDefaultDate(DateTime? date = null)
        {
            return date.HasValue ? date.Value : DateTime.Today;
        }
    }
}
