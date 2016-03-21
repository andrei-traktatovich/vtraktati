using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain.Extensions
{
    public static class ExtendIQueryable
    {
        public static Expression<Func<IQueryable<T>, IQueryable<T>>> Existing<T>(this IQueryable<T> source) where T : class, ISoftDelete
        {
            return x => source.Where(item => !item.IsDeleted);            
        }
    }
}
