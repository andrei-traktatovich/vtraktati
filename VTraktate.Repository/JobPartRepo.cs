using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.Netting;
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.Domain.Interfaces;
using VTraktate.Domain.Extensions;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class JobPartRepo : Repo<JobPart>
    {
        public JobPartRepo(TraktatContext context)
            : base(context)
        { }

        INettingManager NettingManager;
        public override IQueryable<JobPart> GetGraphs(System.Linq.Expressions.Expression<Func<JobPart, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Currency)
                .Include(x => x.DaughterJob)
                .Include(x => x.Job)
                .Include(x => x.Provider.Office)
                .Include(x => x.Job.Order)
                .Include(x => x.JobType)
                .Include(x => x.Language)
                .Include(x => x.Status)
                .Include(x => x.UOM);
        }
        public override void AddOrUpdate(JobPart entity)
        {
            entity.EnsureNonNullFinalVolumeAndPricing();
            base.AddOrUpdate(entity);
        }

        public override async Task DeleteAsync(int id)
        {
            var person = GetGraphs(x => x.Id == id).SingleOrDefault();
            if (person == null)
                throw new InvalidOperationException(string.Format("paricipant with id {0} not found", id));
            if (person.DaughterJob != null)
            {
                Context.Entry(person.DaughterJob).State = EntityState.Deleted;
            }
            Context.Entry(person).State = EntityState.Deleted;
        }
    }
}
