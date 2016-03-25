using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Providers
{
    public interface IProviderManagerFactory
    {
        IProviderManager Create(ICerberos cerberos, IRepo<Provider> providerRepo);
    }

    public interface IProviderManager
    {
        Task<OperationResult<Provider>> FindAndDeleteAsync(int providerId);
    }
}
