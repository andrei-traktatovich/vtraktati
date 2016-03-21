using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Core.Infrastructure;

namespace VTraktate.Controllers
{
    public class ProviderSoftController : ApiController
    {
        public ProviderSoftController(IRepo<Provider> repo, IRepo<ProviderSoft> softRepo)
        {
            Repo = repo;
            SoftRepo = softRepo;
        }
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }
        protected IRepo<Provider> Repo;
        protected IRepo<ProviderSoft> SoftRepo;

        [HttpPost]
        [Route("api/provider/{providerId}/soft")]
        public async Task<IHttpActionResult> Post(int providerId, [FromBody] ProviderAddSoftBindingModel model)
        {
            var provider = Repo.GetGraphs(x => x.Id == providerId).SingleOrDefault();
            if (provider == null)
                return BadRequest("Не найден провайдер с ID " + providerId.ToString());

            var result = new List<IdNamePair>();

            foreach (var id in model.Ids)
            {

                var soft = await SoftRepo.FindByIdAsync(id);

                if (soft == null)
                    return BadRequest("Софтина с ID " + id.ToString() + " не найдена");

                if (provider.Soft.Any(x => x.Id == soft.Id))
                    return BadRequest("Такая софтина уже есть");

                provider.Soft.Add(soft);
                result.Add(new IdNamePair { Id = soft.Id, Name = soft.Name });
            }

            await Repo.SaveAsUserAsync(UserId);

            return Ok(result);
        }

        [Route("api/provider/{providerId}/soft/{softId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int providerId, int softId)
        {
            var provider = await Repo.GetGraphs(x => x.Id == providerId).SingleOrDefaultAsync();

            if(provider == null)
                return BadRequest("Не найден провайдер с ID " + providerId.ToString());
            
            var soft = provider.Soft.Where(x => x.Id == softId).SingleOrDefault();
            
            if (soft == null)
                return BadRequest("Софтина с ID " + softId.ToString() + " не найдена");

            provider.Soft.Remove(soft);

            await Repo.SaveAsUserAsync(UserId);

            return Ok(); 
        }
    }

    public class ProviderAddSoftBindingModel
    {
        public int[] Ids { get; set; }
    }
}
