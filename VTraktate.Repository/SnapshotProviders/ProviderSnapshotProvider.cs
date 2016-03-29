using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.DataAccess;
using VTraktate.DataAccess.ExtensionMethods;
using VTraktate.Domain;
using System.Data.Entity;

namespace VTraktate.Repository.SnapshotProviders
{
    public class ProviderSnapshotProvider : ISnapshotProvider<ProviderSnapshot>
    {
        private TraktatContext _context;
        public ProviderSnapshotProvider(TraktatContext context)
        {
            _context = context;
        }

        public virtual IQueryable<ProviderSnapshot> Get(DateTime? date = null)
        {
            var existingEmployments = _context.Employments
                .Include(x => x.Office)
                .Include(x => x.Title)
                .Include(x => x.Status)
                .ExistingCurrent();

            var existingCalendarPeriods = _context.CalendarPeriods
                .Include(x => x.Status)
                .Include(x => x.CurrentOffice)
                .Current();

            var currentFreelances = _context.Freelances.ExistingCurrent()
                .Include(x => x.FreelanceStatus);

            var query = from provider in _context.Providers.Include(x => x.Soft).Existing()
                        join existingEmployment in existingEmployments on provider.Id equals existingEmployment.ProviderId into innerEmpl
                        from innerExistingEmployment in innerEmpl.DefaultIfEmpty()
                        join existingCalendarPeriod in existingCalendarPeriods on provider.Id equals existingCalendarPeriod.ProviderId into innerCal
                        from innerExistingCalendarPeriod in innerCal.DefaultIfEmpty()
                        join currentFreelance in currentFreelances on provider.Id equals currentFreelance.ProviderID into innerFreelances
                        from innerCurrentFreelance in innerFreelances.DefaultIfEmpty()
                        select new ProviderSnapshot
                        {
                            Provider = provider,
                            CurrentEmployment = innerExistingEmployment,
                            CurrentCalendarPeriod = innerExistingCalendarPeriod,
                            CurrentFreelance = innerCurrentFreelance
                        };
            
            return query;
        }


        public Task<ProviderSnapshot> GetByRootIdAsync(int id)
        {
            return Get().SingleOrDefaultAsync(x => x.Provider.Id == id);
        }

    }

    public class ServiceProviderSnapshotProvider : ISnapshotProvider<ServiceProviderSnapshot>
    {
        private TraktatContext _context;
        private ProviderSnapshotProvider _providerSnapshotProvider;
        public ServiceProviderSnapshotProvider(TraktatContext context, ProviderSnapshotProvider providerSnapshotProvider)
        {
            _context = context;
            _providerSnapshotProvider = providerSnapshotProvider;
        }

        public IQueryable<ServiceProviderSnapshot> Get(DateTime? date = null)
        {
            var providers = _providerSnapshotProvider.Get();

            IQueryable<ServiceProviderSnapshot> query = providers.Select(x => new ServiceProviderSnapshot { 
                Provider = x.Provider,
                CurrentCalendarPeriod = x.CurrentCalendarPeriod,
                CurrentEmployment = x.CurrentEmployment,
                CurrentFreelance = x.CurrentFreelance,
                BusyStatus = null,
                Groups = null,
                IsPromoted = false 
            });

            // do something here 

            return query;
        }

        public IQueryable<ServiceProviderRateSnapshot> Get(int serviceId, int? languageId = null)
        {
            var query = Get();

            //if (_context.ServiceTypes.Single(x => x.Id == serviceId).SpecifyLanguage)
            //{
            //    if (languageId == null)
            //        return query;
            //}

            //var result = from serviceProvider in query
            //             join service in _context.Services on serviceProvider.Provider.Id equals service.ProviderId 

            return null;       
        }
        
        public Task<ServiceProviderSnapshot> GetByRootIdAsync(int id)
        {
            return Get().SingleOrDefaultAsync(x => x.Provider.Id == id);
        }
    }

     

}