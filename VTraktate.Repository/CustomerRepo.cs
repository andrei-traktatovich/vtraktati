using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class CustomerRepo : Repo<Customer>, ICustomerRepo
    {
        public CustomerRepo(VTraktate.DataAccess.TraktatContext ctx) : base(ctx)
        {

        }
        public override IQueryable<Customer> GetGraphs(System.Linq.Expressions.Expression<Func<Customer, bool>> predicate = null)
        {
            // TODO: COMPLETE THIS !!! 
            return Get(predicate)
                .Include(x => x.ContactPersons);
        }

        // todo: make a generic method to find any IEntity ? 
        public Office GetOfficeById(int officeId)
        {
            return Context.Offices.Find(officeId);
        }
    }
}
