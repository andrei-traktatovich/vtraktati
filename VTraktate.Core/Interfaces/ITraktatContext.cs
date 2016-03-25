using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces
{
    public interface ITraktatContext
    {
        IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntity;

        Task<AspNetUser> GetUserGraphAsync(int userId);

        Task<T> GetByIdAsync<T>(int id)
            where T : class, IEntity;
    }
}
