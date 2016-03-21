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
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace VTraktate.Controllers
{
    
    public class ProviderLanguageController : ApiController
    {
        // TODO: encapsulate !!! 
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }
        public LanguageManager LanguageManager { get; private set; }
        public ProviderLanguageController(LanguageManager languageManager)
        {
            LanguageManager = languageManager;
        }

        [HttpPost]
        [Route("api/providerService/{serviceId}/languages")]
        public async Task<IHttpActionResult> Post(int serviceId, [FromBody] ProviderAddLanguagesBindingModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult<IEnumerable<ServiceLanguageInfo>> result = await LanguageManager.AddLanguagesAsync(serviceId, model, UserId);
                if (result.Success)
                {
                    var newComers = Mapper.Map<IEnumerable<ServiceLanguageViewModel>>(result.Data);
                    return Ok(newComers);
                }
                else
                    return BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest("Некорректные данные. Сохранение невозможно");
        }

        [Route("api/providerLanguage/{languageId}")]
        public async Task<IHttpActionResult> Put(int languageId, [FromBody] ProviderUpdateLanguageBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await LanguageManager.UpdateAsync(model, UserId);
                if (result.Success)
                {
                    var language = AutoMapper.Mapper.Map<ServiceLanguageViewModel>(result.Data);
                    return Ok(language);
                }
                else
                    return BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest("Данные неполны или некорректны. Сохранение данных невозможно");
        }

        [HttpDelete]
        [Route("api/language/{providerLanguageId}")]
        public async Task<IHttpActionResult> Delete(int providerLanguageId)
        {
            try
            {
                await LanguageManager.DeleteAsync(providerLanguageId, UserId);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }
        
    }

    public class LanguageManager
    {
        public IServiceLanguageInfoRepo Repo { get; private set; }
        public LanguageManager(IServiceLanguageInfoRepo repo)
        {
            Repo = repo;
        }

        const string CANNOT_ADD_LANGUAGES = "Невозможно добавить все или некоторые языковые направления: ";

        public async Task DeleteAsync(int id, int userId)
        {
            await Repo.DeleteAsync(id);
            await Repo.SaveAsUserAsync(userId);
        }
        public async Task<OperationResult<IEnumerable<ServiceLanguageInfo>>> AddLanguagesAsync(int serviceId, ProviderAddLanguagesBindingModel model, int userId)
        {
            var newLanguages = new List<ServiceLanguageInfo>();

            foreach (var id in model.Languages)
            {
                var newLanguage = new ServiceLanguageInfo
                {
                    CreatedDate = DateTime.Now,
                    Comment = model.Comment,
                    LanguagePairId = id,
                    QA = new QA { Comment = model.qaComment, Grade = 0, Stars = (Stars)model.Stars },
                    Rate = new ServiceRate { Minrate = model.MinRate, MaxRate = model.MaxRate },
                    ProductivityMax = model.ProductivityMax,
                    ProductivityMin = model.ProductivityMin, 
                    ServiceId = serviceId,
                    NativeSpeaker = model.NativeSpeaker
                };
                newLanguages.Add(newLanguage);
                Repo.AddOrUpdate(newLanguage);
                // I used to do it in two passes, but it causes error.
                // So I save after each add/update
                try
                {
                    await Repo.SaveAsUserAsync(userId);
                    Repo.BindGrades(newLanguage);
                    await Repo.SaveAsUserAsync(userId);
                }
                catch (Exception ex)
                {
                    return OperationResult<IEnumerable<ServiceLanguageInfo>>.Error(string.Format("{0}{1}.", CANNOT_ADD_LANGUAGES, ex.Message));
                }
            }

            var newLanguageIds = newLanguages.Select(x => x.Id);

            var result = Repo
                    .Get(x => newLanguageIds.Contains(x.Id))
                    .Include(x => x.LanguagePair)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.ModifiedBy)
                    .ToList();

            return OperationResult<IEnumerable<ServiceLanguageInfo>>.Ok(result);
        }

        public async Task<OperationResult<ServiceLanguageInfo>> UpdateAsync(ProviderUpdateLanguageBindingModel model, int UserId)
        {
            var language = Repo.GetGraphs(x => x.Id == model.Id).First();

            language.QA.Comment = model.qaComment;
            language.QA.Stars = model.qaStars;
            language.Rate.Minrate = model.MinRate;
            language.Rate.MaxRate = model.MaxRate;
            language.Comment = model.Comment;
            language.NativeSpeaker = model.NativeSpeaker;
            language.ProductivityMin = model.ProductivityMin;
            language.ProductivityMax = model.ProductivityMax;

            await Repo.SaveAsUserAsync(UserId);

            return OperationResult<ServiceLanguageInfo>.Ok(language);
        }
    }
}
