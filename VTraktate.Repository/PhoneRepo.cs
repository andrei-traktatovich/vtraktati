using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class PhoneRepo : Repo<Phone>
    {
        public PhoneRepo(TraktatContext ctx)
            : base(ctx)
        {

        }

        public override IQueryable<Phone> GetGraphs(System.Linq.Expressions.Expression<Func<Phone, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy);
        }
    }
}
