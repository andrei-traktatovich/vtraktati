using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;
using VTraktate.DataAccess.ExtensionMethods;
using VTraktate.DataAccess;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class EmploymentRepo : Repo<Employment>
    {
        public EmploymentRepo(TraktatContext ctx) : base(ctx)
        {

        }
        public override void AddOrUpdate(Employment entity)
        {
             
            base.AddOrUpdate(entity);
        }

        public override IQueryable<Employment> GetGraphs(System.Linq.Expressions.Expression<Func<Employment, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.Office)
                .Include(x => x.Status)
                .Include(x => x.Title);
        }
    }
}
