using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Orders
{
    public interface IJobManager
    {
        IQueryable<Job> GetGraphs(Expression<Func<Job, bool>> predicate = null);

        Task<Job> FindAsync(int id);

        Task<Job> UpdateAndSave(Job job, int userId);

        Task SaveAsync(int UserId);

        Task DeleteAsync(int id, int userId);
    }
}
