using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class OtherContactRepo : Repo<OtherContact>
    {
        public OtherContactRepo(VTraktate.DataAccess.TraktatContext ctx) : base(ctx)
        {

        }

        public override IQueryable<OtherContact> GetGraphs(System.Linq.Expressions.Expression<Func<OtherContact, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Type);
        }
    }
}
