using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.DataAccess;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Domain.Snapshots;

namespace VTraktate.Repository.SnapshotProviders
{
    public class AccountSnapshotProvider : ISnapshotProvider<AccountSnapshot>
    {
        private TraktatContext _context;
        private ProviderSnapshotProvider _providerSnapshotProvider;
        public AccountSnapshotProvider(TraktatContext context, ProviderSnapshotProvider providerSnapshotProvider)
        {
            _context = context;
            _providerSnapshotProvider = providerSnapshotProvider;
        }
        public IQueryable<AccountSnapshot> Get(DateTime? date = null)
        {
            var providerSnapshots = _providerSnapshotProvider.Get();

            var people = _context.People;

            var aspNetUsers = _context.AspNetUsers.Include(x => x.AspNetRoles);

            var query = from person in people
                        join providerSnapshot in providerSnapshots on person.ProviderId equals providerSnapshot.Provider.Id into inner
                        from innerItem in inner.DefaultIfEmpty()
                        join account in aspNetUsers on person.Id equals account.PersonId into innerAccounts
                        from innerAccount in innerAccounts.DefaultIfEmpty()
                        select new AccountSnapshot { Person = person, AspNetUser = innerAccount, ProviderSnapShot = innerItem };

            return query;
        }

        public async Task<AccountSnapshot> GetByRootIdAsync(int id)
        {
            return await Get().SingleOrDefaultAsync(x => x.AspNetUser.Id == id);
        }
    }
}