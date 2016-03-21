using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.DataAccess;
using VTraktate.Domain;

namespace VTraktate.Repository
{
    public class PersonRepo : Repo<Person>
    {
        public PersonRepo(TraktatContext ctx) : base(ctx)
        {

        }

        public override IQueryable<Person> GetGraphs(System.Linq.Expressions.Expression<Func<Person, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
