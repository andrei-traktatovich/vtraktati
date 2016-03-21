using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;
using VTraktate.Domain.Interfaces;

namespace VTraktate.DataAccess.ExtensionMethods
{
    public static class FilterExpressions
    {
        public static Expression<Func<T, bool>> IsCurrent<T>(DateTime? startDate = null, DateTime? endDate = null)
            where T : class, ICalendarPeriod
        {
            var start = startDate ?? DateTime.Today;
            var end = endDate ?? start;

            if (start == end)
                return y => (y.StartDate <= start)
                && (!y.EndDate.HasValue || y.EndDate.Value >= start); // simple case
            else
                throw new NotImplementedException("IsCurrent<T> : date range test not implemented!!!");
            
        }
        // A completable is only deemed completed if 
        public static Expression<Func<T, bool>> IsPending<T>(DateTime? date = null)
            where T : JobPart
        {
            date = date ?? DateTime.Now;
            return y => (y.StartDate <= date) && (!y.CompletionDate.HasValue) && (y.Status != null && y.Status.IsPending);
        }
        public static IQueryable<T> Pending<T>(this IQueryable<T> source, DateTime? date = null)
            where T : JobPart
        {
            return source.Where(IsPending<T>(date));
        }

        public static IQueryable<T> Current<T>(this IQueryable<T> source, DateTime? startDate = null, DateTime? endDate = null)
            where T : class, ICalendarPeriod
        {
            return source.Where(IsCurrent<T>(startDate, endDate));
        }

        public static Expression<Func<T, bool>> IsExisting<T>()
            where T : class, ISoftDelete
        {
            return x => !x.IsDeleted;
        }

        public static IQueryable<T> Existing<T>(this IQueryable<T> source)
            where T : class, ISoftDelete
        {
            return source.Where(IsExisting<T>());
        }

        public static IQueryable<T> ExistingCurrent<T>(this IQueryable<T> source, DateTime? startDate = null, DateTime? endDate = null)
            where T : class, ISoftDelete, ICalendarPeriod
        {
            return source.Current(startDate, endDate).Existing();
        }

        // TODO: THIS MAY BE A DUPLICATE !!! ! !!! 
        public static IQueryable<JobPart> Pending(this IQueryable<JobPart> source)
        {
            return source.Where(x => x.Status.IsPending);
        }
    }
}
