using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Repository
{
    public class ProviderSoftRepo : Repo<ProviderSoft>
    {
        public ProviderSoftRepo(VTraktate.DataAccess.TraktatContext ctx) : base(ctx)
        {
            Context = ctx;
        }
        public override IQueryable<ProviderSoft> GetGraphs(System.Linq.Expressions.Expression<Func<ProviderSoft, bool>> predicate = null)
        {
            return Get(predicate);
        }
    }
}