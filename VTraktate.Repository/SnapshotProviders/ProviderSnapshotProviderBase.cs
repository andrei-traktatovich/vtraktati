using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.DataAccess;
using VTraktate.DataAccess.ExtensionMethods;
using VTraktate.Domain;

namespace VTraktate.Repository.SnapshotProviders
{
    public abstract class ProviderSnapshotProviderBase
    {
        public ProviderSnapshotProviderBase(TraktatContext context)
        {
            Context = context;
        }

        protected TraktatContext Context { get; private set; }

        protected IQueryable<ProviderSnapshotBase> GetProviderSnapshots(DateTime? date = null)
        {
            var currentEmployments = Context.Employments.ExistingCurrent(date);
            var currentEmployeeCalendarPeriods = Context.CalendarPeriods.Current(date);
            var currentFreelances = Context.Freelances.ExistingCurrent(date);
            var currentFreelanceCalendarPeriods = Context.FreelanceCalendarPeriods.ExistingCurrent(date);
            var currentPromotions = Context.Promotions.Current(date);
            var pendingTasks = Context.JobParts.Current(date); // TODO: add PENDING 

            var result = from provider in Context.Providers.Existing()
                // + promotions
                join item in currentPromotions on provider.Id equals item.PromoteeId into myPromotions
                from promotion in myPromotions.DefaultIfEmpty()
                // + employments
                join item in currentEmployments on provider.Id equals item.ProviderId into myEmployments
                from employment in myEmployments.DefaultIfEmpty()
                // + employee calendar periods
                join item in currentEmployeeCalendarPeriods on provider.Id equals item.ProviderId into
                    myEmployeeCalendarPeriods
                from employeeCalendarPeriod in myEmployeeCalendarPeriods.DefaultIfEmpty()
                // + freelances
                join item in currentFreelances on provider.Id equals item.ProviderID into myCurrentFreelances
                from freelance in myCurrentFreelances.DefaultIfEmpty()
                // + freelance calendar periods
                join item in currentFreelanceCalendarPeriods on provider.Id equals item.ProviderId into
                    myFreelanceCalendarPeriods
                from freelanceCalendarPeriod in myFreelanceCalendarPeriods.DefaultIfEmpty()
                // + services 
                join item in Context.Services on provider.Id equals item.ProviderId into myServices
                from service in myServices.DefaultIfEmpty()
                // + languages
                join item in Context.ServiceLanguageInfos on service.Id equals item.ServiceId into myLanguages
                from language in myLanguages.DefaultIfEmpty()
                // + domains 
                join item in Context.ServiceDomaininfos on language.Id equals item.LanguageId into myDomains
                from domain in myDomains.DefaultIfEmpty()
                select new ProviderSnapshotBase
                {
                    Provider = provider,
                    Promotion = promotion,
                    Employment = employment,
                    EmployeeCalendarPeriod = employeeCalendarPeriod,
                    Freelance = freelance,
                    FreelanceCalendarPeriod = freelanceCalendarPeriod,
                    Service = service,
                    Language = language,
                    Domain = domain
                };
            return result;
        }
    }
}
