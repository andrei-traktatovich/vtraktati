using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using VTraktate.Core.Interfaces.Filtering;
using System.Data.Entity;

namespace VTraktate.Filtering
{
    public class TerminalFilter : Attribute
    { }

    public class SourcePropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public SourcePropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public class QueryFilterService<TSource> : 
        IQueryFilterService<TSource>
        where TSource : class
        
    {
        private IFilteringRules _filteringRules;

        public QueryFilterService(IFilteringRules filteringRules)
        {
            if (filteringRules == null)
                throw new ArgumentNullException("filteringRules");

            _filteringRules = filteringRules;
        }

        class ExtendedPropertyInfo
        {
            public bool IsTerminal { get; set; }
            public PropertyInfo PropertyInfo { get; set; }
        }

        private Dictionary<string, MethodInfo> GetMethods<TReturn>()
        {
            var type = _filteringRules.GetType();

            var objectType = typeof(object);
            var funcType = typeof(TReturn);
            
            var result = (from m in type.GetMethods()
                          let p = m.GetParameters()
                          where p.Length == 1
                              && p[0].ParameterType == objectType
                              && m.ReturnType == funcType
                          select m)
                          .ToDictionary(x => x.Name, x => x);
            return result;
        }

        protected List<TResult> BuildExpressionList<TResult>(object filterModel, bool ignoreTerminalFilters) 
            where TResult : class
        {
            var result = new List<TResult>();

            Type type = filterModel.GetType();

            ExtendedPropertyInfo[] properties = type.GetProperties()
                .Select(x => new ExtendedPropertyInfo 
                {  
                    IsTerminal = x.CustomAttributes.Any(ca => ca.AttributeType == typeof(TerminalFilter)),
                    PropertyInfo = x
                })
                .OrderByDescending(x => x.IsTerminal)
                .ToArray();
            
            var filterFunctions = GetMethods<TResult>();

            foreach (ExtendedPropertyInfo property in properties)
            {
                var propName = property.PropertyInfo.Name;
                if (filterFunctions.ContainsKey(propName))
                {
                    var val = property.PropertyInfo.GetValue(filterModel, null);
                    if (val != null)
                    {
                        var func = filterFunctions[propName];
                        if (func != null)
                        {
                            var item = func.Invoke(_filteringRules, new[] { property.PropertyInfo.GetValue(filterModel, null) }) as TResult;
                            
                            if (item != null)
                                result.Add(item);
                            
                            if (!ignoreTerminalFilters && property.IsTerminal)
                                break;
                        }
                    }
                }
            }
       
            return result;
        }

        public IOrderedQueryable<TResult> Sort<TResult>(IQueryable<TResult> source, ISortModel sortModel)
            where TResult : class
        {
            
            if (source == null)
                return null;

            if (sortModel != null) 
            {
                var orderby = MakeSortExpression(sortModel);
                if (!string.IsNullOrEmpty(orderby))
                    return source.OrderBy(orderby)  as IOrderedQueryable<TResult>;
            }
            // no sort expression, sorting by default
            var defaultSort = source.OrderBy(_filteringRules.DefaultSort);
            return defaultSort as IOrderedQueryable<TResult>;
        }


        private string MakeSortExpression(ISortModel model)
        {
            // one-property sort is supported 
            if (model == null)
                return null;

            var result = model.GetType()
                .GetProperties()
                .Where(x => x.GetValue(model, null) != null
                    && x.GetCustomAttribute<SourcePropertyAttribute>() != null)
                    .Select(item => 
                        string.Format("{0} {1}",
                        item.GetCustomAttribute<SourcePropertyAttribute>().PropertyName, 
                        item.GetValue(model, null).ToString() == "desc" ? " desc" : ""))
                .ToArray();
                
            return string.Join(",", result);
        }

        public IQueryable<TSource> Filter(IQueryable<TSource> source, IFilterModel filterModel, bool ignoreTerminalFilters)
        {
             
            if (filterModel == null)
                return source;

            var expressions = BuildExpressionList<Expression<Func<TSource, bool>>>(filterModel, ignoreTerminalFilters);
            
            if (expressions == null)
                return source;
            
            var result = source;

            foreach(var expression in expressions)
            {
                var expr = expression;
                result = result.Where(expr);
            }

            return result;
            
        }

        public async Task<PagedResult<TResult>> GetPageAsync<TResult>(IOrderedQueryable<TResult> source, 
            int pageNo, 
            int pageSize, Expression<Func<TSource, TResult>> converter)
        {
            if (pageNo < 1)
                pageNo = 1;

            var total = await source.CountAsync();

            var result = await source
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<TResult>
            {
                Total = total,
                Result = result
            };
        }

        public virtual async Task<PagedResult<TResult>> GetFilteredOrderedPageAsync<TResult>(IQueryable<TSource> source,
            IFilterModel filterModel,
            ISortModel sortModel,
            int page, int pageSize,
            Expression<Func<TSource, TResult>> converter,
            bool useDistinctAfterProjection = true,
            bool ignoreTerminalFilters = false
            )
            where TResult : class 
        {
            var filtered = Filter(source, filterModel, ignoreTerminalFilters);

            var result = await GetFilteredOrderedPageAsync<TResult>(filtered, sortModel, page, pageSize, converter, useDistinctAfterProjection);

            return result;
        }

        protected async Task<PagedResult<TResult>> GetFilteredOrderedPageAsync<TResult>(IQueryable<TSource> filtered, 
            ISortModel sortModel,
            int page, int pageSize,
            Expression<Func<TSource, TResult>> converter,
            bool useDistinctAfterProjection = true
            )
            where TResult : class
        {
            IQueryable<TResult> projected;

            if (useDistinctAfterProjection)
                projected = filtered.Select(converter).Distinct();
            else
                projected = filtered.Distinct().Select(converter);

            var ordered = Sort<TResult>(projected, sortModel);

            var result = await GetPageAsync<TResult>(ordered, page, pageSize, converter);

            return result;
        }

    }
}
