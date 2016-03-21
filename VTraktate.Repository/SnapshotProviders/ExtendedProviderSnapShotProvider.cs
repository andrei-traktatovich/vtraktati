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
using VTraktate.Domain.Snapshots;
using System.Data.Entity;

namespace VTraktate.Repository.SnapshotProviders
{
    public class ExtendedProviderSnapShotProvider : ProviderSnapshotProviderBase, ISnapshotProvider<ExtendedProviderSnapshot>
    {
        public ExtendedProviderSnapShotProvider(TraktatContext context) : base(context) { }
        
        public IQueryable<ExtendedProviderSnapshot> Get(DateTime? date = null)
        {
            var pendingTasks = Context.JobParts.Pending(date); // TODO: add PENDING 

            var providerSnapshots = GetProviderSnapshots(date);

            var providersWithPendingJobs = providerSnapshots.GroupJoin(
                    pendingTasks, 
                    snapshot => snapshot.Provider.Id, 
                    jobPart => jobPart.ProviderId,
                    (snapshot, pt) => new 
                    { 
                        snapshot = snapshot, 
                        IsBusy = pt.Count() > 0, 
                        BusyThrough = pt.Max(x => x.EndDate), 
                        PendingJobParts = pt 
                    });

            IQueryable<ExtendedProviderSnapshot> result = providersWithPendingJobs
                .Select(x => new ExtendedProviderSnapshot
            {
                Provider = x.snapshot.Provider,
                Freelance = x.snapshot.Freelance,
                EmployeeCalendarPeriod = x.snapshot.EmployeeCalendarPeriod,
                Employment = x.snapshot.Employment,
                FreelanceCalendarPeriod = x.snapshot.FreelanceCalendarPeriod,
                Language = x.snapshot.Language,
                Promotion = x.snapshot.Promotion,
                Service = x.snapshot.Service,
                IsBusy = x.IsBusy,
                AvailabilityStatusId = x.IsBusy ? ProviderAvailabilityStatus.Busy
                    : (x.snapshot.FreelanceCalendarPeriod != null ?
                        x.snapshot.FreelanceCalendarPeriod.StatusId : ProviderAvailabilityStatus.Free),
                BusyThrough = x.BusyThrough,
                PendingJobParts = x.PendingJobParts
            });

            return result;
        }

        public async Task<ExtendedProviderSnapshot> GetByRootIdAsync(int id)
        {
            var query = Get();
            return await query.SingleOrDefaultAsync(x => x.Provider.Id == id);
        }
    }
}
