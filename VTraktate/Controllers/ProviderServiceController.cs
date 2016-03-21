using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace VTraktate.Controllers
{
     
    
    public class ProviderServiceController : ApiController
    {
        // TODO: encapsulate !!! 
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        ProviderServiceManager ProviderServiceManager;
        public ProviderServiceController(ProviderServiceManager manager)
        {
            ProviderServiceManager = manager;
        }

        [Route("api/providerService/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var result = await ProviderServiceManager.DeleteAsync(id, UserId);
                if (result.Success)
                {
                    return Ok();
                }
                else
                    return BadRequest(result.ErrorMessage);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    
        [Route("api/provider/{id}/services")]
        public async Task<IHttpActionResult> Post(int id, [FromBody] ProviderServiceBindingModel model)
        {
            // ATTN: NAITVE IMPLEMENTATION !!! 
            if (model == null)
                return BadRequest("Нет информации об услуге.");
            var result = await ProviderServiceManager.AddAsync(id, model, UserId);
            if (result.Success)
            {
                var service = AutoMapper.Mapper.Map<ServiceViewModel>(result.Data);
                return Ok(service);
            }
            else
                return BadRequest(result.ErrorMessage);
        }

        [Route("api/providerService/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] ProviderServiceBindingModel model)
        {
            // check model validity !!! 
            var result = await ProviderServiceManager.UpdateAsync(model, UserId);
            if (result.Success)
            {
                var service = AutoMapper.Mapper.Map<ServiceViewModel>(result.Data);
                return Ok(service);
            }
            else
                return BadRequest(result.ErrorMessage);
        }

    }

    public class ProviderServiceManager
    {
        private IServiceRepo Repo;
        public ProviderServiceManager(IServiceRepo repo)
        {
            Repo = repo;
        }
        
        public async Task<OperationResult<bool>> DeleteAsync(int id, int userId)
        {
            var item = await Repo.FindByIdAsync(id);
            if (item != null)
            {
                await Repo.DeleteAsync(id);
                await Repo.SaveAsUserAsync(userId);
                return OperationResult<bool>.Ok(true);
            }
            else
                return OperationResult<bool>.Error("Услуга не найдена.");
        }

        public async Task<OperationResult<Service>> AddAsync(int providerId, ProviderServiceBindingModel model, int userId)
        {
            var newItem = ProviderServiceBindingModel.ToService(model, providerId);

            Repo.AddOrUpdate(newItem);
            await Repo.SaveAsUserAsync(userId);
            
            Repo.BindGrades(newItem);
            await Repo.SaveAsUserAsync(userId);

            var storedItem = Repo.GetGraphs(x => x.Id == newItem.Id).First();

            return OperationResult<Service>.Ok(storedItem);
        }

        public async Task SaveChangesAsync(int userId)
        {
            await Repo.SaveAsUserAsync(userId);
        }

        public async Task<OperationResult<Service>> UpdateAsync(ProviderServiceBindingModel model, int userId)
        {
            // error handling 
            var service = Repo.GetGraphs(x => x.Id == model.Id.Value).First();
            service.ServiceUOMId = model.uomId;
            service.Rate.Minrate = model.MinRate;
            service.Rate.MaxRate = model.MaxRate;
            service.QA.Stars = (Stars)model.qaStars;
            service.QA.Comment = model.Comment;
            service.CurrencyId = model.currencyId;
            
            await SaveChangesAsync(userId);

            return OperationResult<Service>.Ok(service);
        }
    }
}