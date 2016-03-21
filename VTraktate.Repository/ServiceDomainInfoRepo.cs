using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Core.Repository.Interfaces;

namespace VTraktate.Repository
{
    public class ServiceDomainInfoRepo : Repo<ServiceDomainInfo>, IServiceDomainRepo
    {
        public ServiceDomainInfoRepo(TraktatContext ctx) : base(ctx)
        {

        }

        public override IQueryable<ServiceDomainInfo> GetGraphs(System.Linq.Expressions.Expression<Func<ServiceDomainInfo, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Domain);
        }

        public void BindGrades(ServiceDomainInfo item)
        {
            var primaryGrades = Context.Grades.Where(x => x.ServiceLanguageInfoGradedId.HasValue && x.ServiceLanguageInfoGradedId.Value == item.LanguageId
                && x.PrimaryDomainId.HasValue && x.PrimaryDomainId.Value == item.DomainId);

            BindPrimaryDomain(item, primaryGrades);

            var secondaryGrades = Context.Grades.Where(x => x.ServiceLanguageInfoGradedId.HasValue && x.ServiceLanguageInfoGradedId.Value == item.LanguageId
                && x.SecondaryDomainId.HasValue && x.SecondaryDomainId.Value == item.DomainId);

            BindSecondaryDomain(item, secondaryGrades);

            var allGrades = primaryGrades.Union(secondaryGrades);


            item.QA.Grade = GetAvg(allGrades);

            var language = Context.ServiceLanguageInfos.Include(x => x.Grades).Where(x => x.Id == item.LanguageId).SingleOrDefault();
            language.QA.Grade = GetAvg(language.Grades);

            var service = Context.Services.Include(x => x.Grades).Where(x => x.Id == language.ServiceId).SingleOrDefault();
            service.QA.Grade = GetAvg(service.Grades);
        }

        private void BindPrimaryDomain(ServiceDomainInfo item, IEnumerable<Grade> grades)
        {
            // bind grades
            foreach (var grade in grades)
                grade.PrimaryDomainGradedId = item.Id;
        }

        private void BindSecondaryDomain(ServiceDomainInfo item, IEnumerable<Grade> grades)
        {
            // bind grades
            foreach (var grade in grades)
                grade.SecondaryDomainGradedId = item.Id;
        }

        private decimal GetAvg(IEnumerable<Grade> grades)
        {
            if (grades == null || grades.Count() == 0)
                return 0;

            return Math.Round(grades.DefaultIfEmpty().Average(x => (decimal)x.Score), 2);
        }

        public override async Task DeleteAsync(int id)
        {
            var gradesWithPrimaryDomainGrade = await Context.Grades
                .Where(x => x.PrimaryDomainGradedId.HasValue && x.PrimaryDomainGradedId.Value == id)
                .ToListAsync();
            
            gradesWithPrimaryDomainGrade.ForEach(x => x.PrimaryDomainGraded = null);

            var gradesWithSecondaryDomainGrade = await Context.Grades
                .Where(x => x.SecondaryDomainGradedId.HasValue && x.SecondaryDomainGradedId.Value == id)
                .ToListAsync();
            
            gradesWithPrimaryDomainGrade.ForEach(x => x.SecondaryDomainGraded = null);

            await base.DeleteAsync(id);
        }
    }
}
