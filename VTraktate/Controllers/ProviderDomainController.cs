using AutoMapper;
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
    public class ProviderDomainController : ApiController
    {
        // TODO: encapsulate !!! 
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }
        public  DomainsManager DomainsManager { get; private set; }

        public ProviderDomainController(DomainsManager domainsManager)
        {
            DomainsManager = domainsManager;
        }
        public async Task<IHttpActionResult> Delete (int id)
        {
            try
            { 
                await DomainsManager.DeleteAsync(id, UserId);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

        [Route("api/providerLanguage/{id}/Domains")]
        public async Task<IHttpActionResult> Post(int id, [FromBody] ProviderDomainsBindingModel data)
        {
            // sanity checks
            if (data == null)
                return BadRequest("Недостаточно информации для добавления тематик");
            
            OperationResult<IEnumerable<ServiceDomainInfo>> result = await DomainsManager.AddDomainsAsync(id, data, UserId);
            if (result.Success)
            {
                var newComers = Mapper.Map<IEnumerable<ServiceDomainViewModel>>(result.Data);
                return Ok(newComers);
            }
            else
                return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        [Route("api/providerDomain/{domainId}")]
        public async Task<IHttpActionResult> Put(int domainId, [FromBody] ProviderDomainUpdateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult<ServiceDomainInfo> result = await DomainsManager.UpdateAsync(domainId, model, UserId);
                if (result.Success)
                {
                    var newComer = Mapper.Map<ServiceDomainViewModel>(result.Data);
                    return Ok(newComer);
                }
                else
                    return BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest("Некорректные данные. Сохранение невозможно.");
        }
    }

    public class DomainsManager
    {
        public IServiceDomainRepo Repo { get; private set; }

        public DomainsManager(IServiceDomainRepo repo)
        {
            Repo = repo;
        }

        const string CANNOT_ADD_DOMAINS = "Невозможно добавить тематики: ";
        public async Task<OperationResult<IEnumerable<ServiceDomainInfo>>> AddDomainsAsync(int serviceLanguageInfoId, ProviderDomainsBindingModel domains, int userId)
        {

            var newDomains = new List<ServiceDomainInfo>();

            foreach(var id in domains.DomainIds)
            {
                var newDomain = new ServiceDomainInfo
                {
                     CreatedDate = DateTime.Now,
                     DomainId = id,
                     QA = new QA { Comment = domains.Comment, Grade = 0.00M, Stars = (Stars)domains.Stars },
                     LanguageId = serviceLanguageInfoId
                };
                newDomains.Add(newDomain);
                Repo.AddOrUpdate(newDomain);
                try
                {
                    await Repo.SaveAsUserAsync(userId);
                    Repo.BindGrades(newDomain);
                    await Repo.SaveAsUserAsync(userId);
                }
                catch (Exception ex)
                {
                    return OperationResult<IEnumerable<ServiceDomainInfo>>.Error(string.Format("{0}{1}.", CANNOT_ADD_DOMAINS, ex.Message));
                }
            };

            var newDomainIds = newDomains.Select(x => x.Id);

            var result = Repo
                    .Get(x => newDomainIds.Contains(x.Id))
                    .Include(x => x.Domain)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.ModifiedBy)
                    .ToList();

            return OperationResult<IEnumerable<ServiceDomainInfo>>.Ok(result);
        }
        public async Task DeleteAsync(int id, int userId)
        {
            await Repo.DeleteAsync(id);
            await Repo.SaveAsUserAsync(userId);
        }

        public async Task<OperationResult<ServiceDomainInfo>> UpdateAsync(int domainId, ProviderDomainUpdateBindingModel model, int UserId)
        {
            var domain = await Repo.FindByIdAsync(domainId);
            domain.QA.Stars = model.Stars;
            domain.QA.Comment = model.Comment;
            try
            {
                await Repo.SaveAsUserAsync(UserId);
            }
            catch(Exception ex)
            {
                return OperationResult<ServiceDomainInfo>.Error(ex.Message);
            }
            
            Repo.BindGrades(domain);

            var graph = await Repo.GetGraphs(x => x.Id == domainId).FirstAsync();

            return OperationResult<ServiceDomainInfo>.Ok(graph);
        }
    }


}