using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Core.Interfaces.Filtering
{
    public interface IQueryFilterService<TSource>
        where TSource : class
    {
        Task<PagedResult<TResult>> GetFilteredOrderedPageAsync<TResult>(IQueryable<TSource> source,
            IFilterModel filterModel,
            ISortModel sortModel,
            int page, int pageSize,
            Expression<Func<TSource, TResult>> projection,
            bool useDistinctAfterProjection = true,
            bool ignoreTerminalFilters = false)
            where TResult : class;
    }

    public class PagedResult<T>
    {
        public int Total { get; set; }
        public List<T> Result { get; set; }
        public object Payload { get; set; }
    }


        
}
