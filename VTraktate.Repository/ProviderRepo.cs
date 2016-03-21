using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Data.Entity; 

namespace VTraktate.Repository
{
    public class ProviderRepo : Repo<Provider>, IRepo<Provider>
    {
        public ProviderRepo(TraktatContext ctx) : base(ctx) { }


        public override IQueryable<Provider> GetGraphs(Expression<Func<Provider, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.Services.Select(y => y.ServiceUOM))
                .Include(x => x.Services.Select(y => y.Languages.Select(z => z.Domains.Select(p => p.Domain))))

                .Include(x => x.ContactPersons.Select(cp => cp.CreatedBy))
                .Include(x => x.ContactPersons.Select(cp => cp.ModifiedBy))

                .Include(x => x.ContactPersons.Select(cp => cp.Emails.Select(em => em.CreatedBy)))
                .Include(x => x.ContactPersons.Select(cp => cp.Emails.Select(em => em.ModifiedBy)))
                .Include(x => x.ContactPersons.Select(cp => cp.Phones.Select(ph => ph.CreatedBy)))
                .Include(x => x.ContactPersons.Select(cp => cp.Phones.Select(ph => ph.ModifiedBy)))
                .Include(x => x.ContactPersons.Select(cp => cp.Phones.Select(ph => ph.Type)))

                .Include(x => x.ContactPersons.Select(cp => cp.OtherContacts.Select(oc => oc.Type)))
                .Include(x => x.ContactPersons.Select(cp => cp.OtherContacts.Select(oc => oc.CreatedBy)))
                .Include(x => x.ContactPersons.Select(cp => cp.OtherContacts.Select(oc => oc.ModifiedBy)))

                .Include(x => x.Employments)
                .Include(x => x.Employments.Select(cp => cp.Title))
                .Include(x => x.Employments.Select(cp => cp.Status))
                .Include(x => x.Employments.Select(cp => cp.Office))
                .Include(x => x.Employments.Select(cp => cp.CreatedBy))
                .Include(x => x.Employments.Select(cp => cp.ModifiedBy))

                .Include(x => x.FreelanceCalendarPeriods)
                .Include(x => x.FreelanceCalendarPeriods.Select(y => y.Status))

                .Include(x => x.Services.Select(y => y.CreatedBy))
                .Include(x => x.Services.Select(y => y.ModifiedBy))

                .Include(x => x.Services.Select(y => y.Languages.Select(p => p.ModifiedBy)))
                .Include(x => x.Services.Select(y => y.Languages.Select(p => p.CreatedBy)))
                .Include(x => x.Services.Select(y => y.Languages.Select(z => z.Domains.Select(p => p.ModifiedBy))))
                .Include(x => x.Services.Select(y => y.Languages.Select(z => z.Domains.Select(p => p.CreatedBy))))

                .Include(x => x.Soft)
                .Include(x => x.Region);

        }
    }
}
