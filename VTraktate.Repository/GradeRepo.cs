using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;

namespace VTraktate.Repository
{
    public class GradeRepo : Repo<Grade>
    {
        public GradeRepo(VTraktate.DataAccess.TraktatContext ctx) : base(ctx)
        { }
        private const int WRITTEN_TRANSLATION = 1;
        public override void AddOrUpdate(Grade entity)
        {
            base.AddOrUpdate(entity);
        }

        private async Task BindToProvider(Grade entity)
        {
            // make async
            // check for null
            var provider = await LocateProviderGraphAsync(entity);
            
            if (provider == null)
                throw new InvalidOperationException("Несуществующий провайдер с id = " + entity.ProviderId);

            BindGradeToService(provider, entity);            
        }
        
        private async Task<Provider> LocateProviderGraphAsync(Grade entity)
        {
            return await Context.Providers
                            .Include(x => x.Services.Select(y => y.Languages.Select(z => z.Domains)))
                            .Include(x => x.Services.Select(y => y.ServiceType))
                            .SingleOrDefaultAsync(x => x.Id == entity.ProviderId);
        }
 
        private void BindGradeToService(Provider provider, Grade entity)
        {
            if (provider.Services != null)
            {
                var service = provider.Services.Where(x => x.ServiceTypeId == entity.ServiceTypeId).SingleOrDefault();
                if (service != null)
                {
                    entity.ServiceGradedId = service.Id;
                    BindGradeToLanguage(service, entity);
                }
                else
                {
                    entity.ServiceGradedId = null;
                    entity.ServiceLanguageInfoGradedId = null;
                    entity.PrimaryDomainGradedId = null;
                    entity.SecondaryDomainGradedId = null;
                }
            }
        }

        private void BindGradeToLanguage(Service service, Grade entity)
        {
            int? value = null;
            int? primaryDomainGradedId = null;
            int? secondaryDomainGradedId = null;

            if(service.ServiceType.SpecifyLanguage && service.Languages != null)
            {
                var language = service.Languages.Where(x => x.LanguagePairId == entity.LanguagePairId).SingleOrDefault();
                if(language != null)
                {
                    value = language.Id;
                    primaryDomainGradedId = LocateDomainId(language, entity, entity.PrimaryDomainId);
                    secondaryDomainGradedId = LocateDomainId(language, entity, entity.SecondaryDomainId);
                }
            }
            entity.ServiceLanguageInfoGradedId = value;
            entity.PrimaryDomainGradedId = primaryDomainGradedId;
            entity.SecondaryDomainGradedId = secondaryDomainGradedId;
        }

        private int? LocateDomainId(ServiceLanguageInfo language, Grade entity, int? domainId)
        {
            if(language.Domains != null && domainId != null)
            {
                var domain = language.Domains.Where(x => x.DomainId == domainId.Value).SingleOrDefault();
                if (domain != null)
                    return domain.Id;
            }
            return null;
        }

        public async Task SaveLegacyGradeAndUpdateGradesAsync(Grade entity, int userId)
        {
            
            await BindToProvider(entity);

            AddOrUpdate(entity);

            await base.SaveAsUserAsync(userId);

            RecalculateScores(entity);

            await base.SaveAsUserAsync(userId);
        }

        protected override void ProcessChanges(Grade existingItem, Grade newItem)
        {
            RecalculateScores(existingItem, true);
            
            newItem.CreatedById = existingItem.CreatedById;
            newItem.CreatedDate = existingItem.CreatedDate;
            
            base.ProcessChanges(existingItem, newItem);
        }
        

        private void RecalculateScores(Grade entity, bool exclude = false)
        {
            int? id = exclude ? entity.Id : default(int?);

            if (entity.ServiceGraded != null)
                entity.ServiceGraded.QA.Grade = Avg(entity.ServiceGraded.Grades, id);
            if (entity.ServiceLanguageInfoGraded != null)
                entity.ServiceLanguageInfoGraded.QA.Grade = Avg(entity.ServiceLanguageInfoGraded.Grades, id);
            if (entity.PrimaryDomainGraded != null)
                entity.PrimaryDomainGraded.QA.Grade = Avg(entity.PrimaryDomainGraded.GradesAsPrimary, id);
            if (entity.SecondaryDomainGraded != null)
                entity.SecondaryDomainGraded.QA.Grade = Avg(entity.SecondaryDomainGraded.GradesAsSecondary, id);
        }

        private decimal Avg(ICollection<Grade> gradeCollection, int? exceptId = null)
        {
            if (gradeCollection == null)
                return 0;

            var items = gradeCollection.Where(x => !x.IsDeleted);
            if (exceptId.HasValue)
                items = items.Where(x => x.Id != exceptId.Value);
            if (items.Count() > 0)
                return Math.Round(items.Average(x => (decimal)x.Score), 2);
            else return 0;
        }
    
        public override IQueryable<Grade> GetGraphs(Expression<Func<Grade, bool>> predicate = null)
        {
            return Get(predicate)
                .Include(x => x.CreatedBy)
                .Include(x => x.LanguagePair)
                .Include(x => x.PrimaryDomain)
                .Include(x => x.SecondaryDomain)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Provider)
                .Include(x => x.JobPart)
                .Include(x => x.ServiceGraded)
                .Include(x => x.ServiceLanguageInfoGraded)
                .Include(x => x.PrimaryDomainGraded)
                .Include(x => x.SecondaryDomainGraded);
        }

        public async Task DeleteGradeAndUpdateGradesAsync(int id, int userId)
        {
            var grade = await GetGraphs(x => x.Id == id).SingleOrDefaultAsync();
            await DeleteAsync(id);
            await SaveAsUserAsync(userId);
            RecalculateScores(grade);
            await SaveAsUserAsync(userId);
        }
    }
}
