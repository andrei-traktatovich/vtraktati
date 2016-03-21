using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DataAccess
{
    public static class ExtendIQueryable
    {
        public class PagedResult<T>
        {
            public int Total { get; set; }
            public List<T> Result { get; set; }
        }


        public static PagedResult<TResult> GetPage<TSource, TResult>(this IQueryable<TSource> source, int pageNo, int pageSize, Func<TSource, TResult> converter)
        {
            if (pageNo < 1)
                throw new ArgumentOutOfRangeException("pageNo");

            var pagedResult = source.Skip(pageSize * (pageNo - 1)).Take(pageSize);

            return new PagedResult<TResult>
            {
                Total = source.Count(),
                Result = pagedResult.Select(converter).ToList()
            };
        }
    }
}
