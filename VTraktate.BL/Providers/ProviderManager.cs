using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Interfaces;
using VTraktate.Core.Interfaces.BusinessLogic.Providers;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Providers
{
    public class ProviderManager : IProviderManager
    {
        public ICerberos Cerberos { get; private set; }
        public IRepo<Provider> Repo { get; private set; }

        public ProviderManager(ICerberos cerberos, IRepo<Provider> providerRepo)
        {
            Cerberos = cerberos;
            Repo = providerRepo;
        }

        public async Task<OperationResult<Provider>> FindAndDeleteAsync(int providerId)
        {
            if (!await Cerberos.CanDeleteProvidersAsync())
            {
                return OperationResult<Provider>.Unauthorized("У пользователя нет достаточных прав для удаления исполнителей");
            }

            var provider = await Repo.FindByIdAsync(providerId);

            if (EntityUtils.IsNull(provider))
            {
                return OperationResult<Provider>.NotFound(String.Format("не найден исполнитель с Id {0}", providerId));
            }

            Repo.DeleteItem(provider);

            await Repo.SaveAsUserAsync(Cerberos.UserId);
            return OperationResult<Provider>.Ok(provider);
        }
    }

}
