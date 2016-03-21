using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.Domain.Extensions;
using System.Data.Entity;
using VTraktate.DataAccess.ExtensionMethods;

namespace VTraktate.Repository
{

    public class AccountRepo : Repo<Person>, IAccountRepo
    {
        public AccountRepo(TraktatContext ctx) : base(ctx) { }

        public override IQueryable<Person> GetGraphs(Expression<Func<Person, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
