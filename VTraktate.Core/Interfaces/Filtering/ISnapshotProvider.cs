using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Core.Interfaces.Filtering
{
    public interface ISnapshotProvider<TSnapshot> 
    {
        IQueryable<TSnapshot> Get(DateTime? date = null);

        Task<TSnapshot> GetByRootIdAsync(int id);

    }
}
