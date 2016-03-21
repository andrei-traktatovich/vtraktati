using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Linq.Expressions;
using System.Data.Entity;
using VTraktate.Core.Repository.Interfaces;

namespace VTraktate.Repository
{
    public class ProviderServiceRepo : Repo<Service>, IServiceRepo
    {
        public ProviderServiceRepo(TraktatContext ctx) : base(ctx)
        {

        }

        public override IQueryable<Service> GetGraphs(Expression<Func<Service, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Currency)
                .Include(x => x.ServiceUOM)
                .Include(x => x.ServiceType);
        }

        public override async Task DeleteAsync(int id)
        {
            // manually set serviceGradedId to null because otherwise it violates reference constraint, as EF doesn't do nulling for me.
            var grades = await Context.Grades.Where(x => x.ServiceGraded.Id == id).ToListAsync();
            
            grades.ForEach(x => 
            {
                // need to do this because EF doesn't put in nulls automatically, 
                // set to null on delete not implemented
                x.ServiceGradedId = null;
                x.ServiceLanguageInfoGradedId = null;
                x.PrimaryDomainGradedId = null;
                x.SecondaryDomainGradedId = null;
            });

            await base.DeleteAsync(id);
        }
        public void BindGrades(Service item)
        {
            var someGrades = Context.Grades.Where(x => x.ProviderId == item.ProviderId && x.ServiceTypeId == item.ServiceTypeId);
            
            BindToService(item, someGrades);
            // calculate average
            item.QA.Grade = GetAvg(someGrades);
        }

        private void BindToService(Service service, IEnumerable<Grade> grades)
        {
            // bind grades
            foreach (var grade in grades)
                grade.ServiceGradedId = service.Id;
        }

        private decimal GetAvg(IEnumerable<Grade> grades)
        {
            if (grades == null || grades.Count() == 0)
                return 0;

            return Math.Round(grades.DefaultIfEmpty().Average(x => (decimal)x.Score),2);
        }
    }
}