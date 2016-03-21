using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces;
using VTraktate.Domain.Interfaces;

namespace VTraktate.BL
{
    public class CalendarService<T> : ICalendarService<T>
        where T : class, ICalendarPeriod, ISoftDelete
    {
        public enum CalendarTimeScales
        {
            Day = 60 * 24,
            Hour = 60,
            Minute = 1
        }

        public CalendarService(CalendarTimeScales timeScale, Func<T, T> cloneFactory)
        {
            TimeScale = timeScale;
            Clone = cloneFactory;
        }

        public CalendarTimeScales TimeScale { get; private set; }

        Func<T, T> Clone;
        private int _minTimeSpan { get { return (int)TimeScale; } }

        public virtual void RoundDateTimes(T period)
        {
            period.StartDate = RoundDateTime(period.StartDate, TimeScale);
            if (period.EndDate.HasValue)
            {
                period.EndDate = RoundDateTime(period.EndDate.Value, TimeScale);
            }
        }

        public DateTime RoundDateTime(DateTime value, CalendarTimeScales scale)
        {
            int hour = scale == CalendarTimeScales.Day ? 0 : value.Hour;
            int minute = scale == CalendarTimeScales.Minute ? value.Minute : 0;
            return new DateTime(value.Year, value.Month, value.Day, hour, minute, 0);
        }

        private void ToLocalTime(T period)
        {
            period.StartDate = period.StartDate.ToLocalTime();

            if (period.EndDate != null)
                period.EndDate = period.EndDate.Value.ToLocalTime();
        }
        public IEnumerable<T> Insert(IList<T> periods, T newPeriod) 
            
        {
            List<T> result = new List<T>();

            if (periods == null)
                periods = new List<T>();
            
            ToLocalTime(newPeriod);
            
            RoundDateTimes(newPeriod);
            
            if (periods.Count > 0)
            {

                var count = periods.Count - 1;

                for (var i = count; i >= 0; i--)
                {
                    T item = periods[i];

                    if (newPeriod.Contains(item))
                    {
                        item.IsDeleted = true;
                    }
                    else if (item.Contains(newPeriod))
                    {
                        if (newPeriod.EndDate.HasValue)
                        {
                            T newItem = Clone(item);
                            newItem.StartDate = newPeriod.EndDate.Value.AddMinutes(1);
                            result.Add(newItem);
                        }
                        ClipRight(item, newPeriod.StartDate);
                    }
                    else if (item.OverlapsLeft(newPeriod))
                    {
                        ClipRight(item, newPeriod.StartDate);
                    }
                    else if (item.OverlapsRight(newPeriod))
                    {
                        ClipLeft(item, newPeriod.EndDate);
                    }

                }
            }
            result.Add(newPeriod);

            return result;
        }

        private void ClipRight(T item, DateTime newEnd)
        {
            item.EndDate = newEnd.AddMinutes(-1); // (-_minTimeSpan + 1);
        }

        private void ClipLeft(T item, DateTime? newStart)
        {
            if (newStart == null)
                item.IsDeleted = true;
            else
                item.StartDate = newStart.Value.AddMinutes(+_minTimeSpan);
        }
    }

    public static class ICalendarExtensions
    {
        public static bool Contains(this ICalendarPeriod item, ICalendarPeriod other)
        {
            return (item.StartDate <= other.StartDate)
                && (item.EndDate == null
                    || (other.EndDate.HasValue && item.EndDate.Value >= other.EndDate.Value));
        }


        public static bool OverlapsLeft(this ICalendarPeriod item, ICalendarPeriod other)
        {
            return item.StartDate <= other.StartDate && (item.EndDate == null || item.EndDate.Value >= other.StartDate);
        }

        public static bool OverlapsRight(this ICalendarPeriod item, ICalendarPeriod other)
        {
            return item.StartDate >= other.StartDate && (other.EndDate == null || other.EndDate.Value >= item.StartDate);
        }
    }


    
}
