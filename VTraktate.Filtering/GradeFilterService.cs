using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;

namespace VTraktate.Filtering
{
    public class GradeFilterService : QueryFilterService<Grade>
    {
        public GradeFilterService(IFilteringRules filteringRules)
            : base(filteringRules)
        {

        }
        public override async Task<PagedResult<TResult>> GetFilteredOrderedPageAsync<TResult>(IQueryable<Grade> source, 
            IFilterModel filterModel, 
            ISortModel sortModel, 
            int page, int pageSize, 
            Expression<Func<Grade, TResult>> converter, 
            bool useDistinctAfterProjection = true,
            bool ignoreTerminalFilters = false)
        {
            var filtered = Filter(source, filterModel, ignoreTerminalFilters);

            decimal avg = 0.00M;
            
            if (filtered != null && filtered.Count() > 0)
            {
                avg = Math.Round(filtered.DefaultIfEmpty().Average(x => (decimal)x.Score), 2);
            }

            var result = await GetFilteredOrderedPageAsync<TResult>(filtered, sortModel, page, pageSize, converter, useDistinctAfterProjection);
            result.Payload = new { Avg = avg };

            return result;
        }
    }

}
