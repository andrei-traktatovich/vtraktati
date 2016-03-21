using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DAL
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }

    public static class ISoftDeleteExtensions
    {
        public static IQueryable<T> Existing<T>(this IQueryable<T> source) where T : class, ISoftDelete
        {
            return source.Where(x => !x.IsDeleted);
        }
    }
}
