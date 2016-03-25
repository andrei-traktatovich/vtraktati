using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces;
using VTraktate.Core.Interfaces.BusinessLogic.Providers;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Providers
{
    public class ProviderManagerFactory : IProviderManagerFactory
    {
        public IProviderManager Create(ICerberos cerberos, IRepo<Provider> providerRepo)
        {
            return new ProviderManager(cerberos, providerRepo);
        }
    }
}
