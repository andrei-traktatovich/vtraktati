using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.DataAccess;
using System.Linq.Expressions;
using VTraktate.Domain.Interfaces;
using VTraktate.Domain.Extensions;

namespace VTraktate.Repository
{
    public class JobRepo : Repo<Job>, IJobRepo
    {
        public JobRepo(TraktatContext ctx) : base(ctx) { }
        public override IQueryable<Job> GetGraphs(Expression<Func<Job, bool>> predicate = null)
        {
            var query = Context.Jobs
               .Include(x => x.CreatedBy)
               .Include(x => x.ModifiedBy)
               .Include(x => x.Currency)
               .Include(x => x.Domain1)
               .Include(x => x.Domain2)
               .Include(x => x.JobType)
               .Include(x => x.Language)
               .Include(x => x.Status)
               .Include(x => x.UOM)

               .Include(x => x.Order)
               .Include(x => x.Order.CreatedBy)
               .Include(x => x.Order.ModifiedBy)
               .Include(x => x.Order.Customer)
               .Include(x => x.Order.Office)

               .Include(x => x.JobParts)
               .Include(x => x.JobParts.Select(y => y.Currency))
               .Include(x => x.JobParts.Select(y => y.JobType))
               .Include(x => x.JobParts.Select(y => y.Language))
               .Include(x => x.JobParts.Select(y => y.Provider))
               .Include(x => x.JobParts.Select(y => y.Status))
               .Include(x => x.JobParts.Select(y => y.UOM))

               .Include(x => x.ParentJobPart);
            if (predicate != null)
                query = query.Where(predicate);
            return query;
            
        }

        public override void AddOrUpdate(Job entity)
        {
            entity.EnsureNonNullFinalVolumeAndPricing();
            base.AddOrUpdate(entity);
        }

        public override async Task DeleteAsync(int id)
        {
            // TODO: need to deal with participants having daughter jobs ... 
            var item = await base.GetByIdAsync(id);
            var orderId = item.OrderId;
            var order = Context.Orders.Where(x => x.Id == orderId).Include(x => x.Jobs).SingleOrDefault();
            var job = order.Jobs.Where(x => x.Id == id).FirstOrDefault();
            
            Context.Entry(job).State = EntityState.Deleted;
            
            if (order == null)
                throw new InvalidOperationException(string.Format("order with id {0} not found", orderId));
            var isOrderEmpty = !order.Jobs.Any(x => x.DelegatedToOfficeId == null && x.Id != id);
            if (isOrderEmpty)
                Context.Entry(order).State = EntityState.Deleted;
        }
    }  
}
