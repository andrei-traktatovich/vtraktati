using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class ServiceLanguageRepo : Repo<ServiceLanguageInfo>, IServiceLanguageInfoRepo
    {
        public ServiceLanguageRepo(TraktatContext ctx) : base(ctx)
        {

        }

        public override IQueryable<ServiceLanguageInfo> GetGraphs(System.Linq.Expressions.Expression<Func<ServiceLanguageInfo, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.LanguagePair);
        }

        public void BindGrades(ServiceLanguageInfo item)
        {
            var someGrades = Context.Grades.Where(x => x.ServiceGradedId.HasValue && x.ServiceGradedId.Value == item.ServiceId
                && x.LanguagePairId == item.LanguagePairId);

            BindToLanguage(item, someGrades);
            // calculate average
            item.QA.Grade = GetAvg(someGrades);
            
            // fuck
            var service = Context.Services.Include(x => x.Grades).Where(x => x.Id == item.ServiceId).SingleOrDefault();
            service.QA.Grade = GetAvg(service.Grades);
        }

        private void BindToLanguage(ServiceLanguageInfo item, IEnumerable<Grade> grades)
        {
            // bind grades
            foreach (var grade in grades)
                grade.ServiceLanguageInfoGradedId = item.Id;
        }

        private decimal GetAvg(IEnumerable<Grade> grades)
        {
            if (grades == null || grades.Count() == 0)
                return 0;

            return Math.Round(grades.DefaultIfEmpty().Average(x => (decimal)x.Score), 2);
        }

        public override async Task DeleteAsync(int id)
        {
            var grades = await Context.Grades.Where(x => x.ServiceLanguageInfoGradedId.HasValue && x.ServiceLanguageInfoGradedId.Value == id).ToListAsync();
            
            grades.ForEach(x =>
                {
                    // need to do this because EF doesn't put in nulls automatically, 
                    // set to null on delete not implemented
                    x.ServiceLanguageInfoGradedId = null;
                    x.PrimaryDomainGradedId = null;
                    x.SecondaryDomainGradedId = null;
                });
            
            await base.DeleteAsync(id);
        }
    }
}
