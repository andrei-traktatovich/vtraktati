using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
 
namespace VTraktate.DAL
{
    public static class IQueryableExtensions
    {
        public class PagedResult<T> 
        {
            public int Total { get; set; }
            public List<T> Result { get; set; }
        }
        public static async Task<PagedResult<TResult>> GetPageAsync<TSource, TResult>(this IQueryable<TSource> source, int pageNo, int pageSize, Func<TSource, TResult> converter) 
        {
            if (pageNo < 1)
                throw new ArgumentOutOfRangeException("pageNo");
            
            var pagedResult = source.Skip(pageSize * (pageNo - 1)).Take(pageSize).AsEnumerable();
            
            return new PagedResult<TResult>
            {
                Total = source.Count(),
                Result = pagedResult.Select(converter).ToList()
            };
        }

         
    }
}
