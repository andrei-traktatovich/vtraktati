using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;
using VTraktate.Models;
using VTraktate.Core.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using VTraktate.Repository;

namespace VTraktate.Controllers
{
    public class ServiceGradeController : ApiController
    {
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        // temp solution for demo purposes only
        public ServiceGradeController(VTraktate.DataAccess.TraktatContext ctx, 
            IQueryFilterService<Grade> filterService, GradeRepo repo)
        {
            _ctx = ctx;
            _filterService = filterService;
            _repo = repo;
        }

        private VTraktate.DataAccess.TraktatContext _ctx;
        private IQueryFilterService<Grade> _filterService;
        private GradeRepo _repo;

        [HttpGet]
        [Route("api/grades")]
        public async Task<IHttpActionResult> Get([FromUri] GradeFilterModel model)
        {
            var grades = _repo.GetGraphs().Where(x => !x.IsDeleted);
            var page = await _filterService.GetFilteredOrderedPageAsync<GradeViewModel>(grades, 
                model.Filter, 
                model.Sorting, 
                model.Page, 
                model.Count, 
                GradeViewModel.FromGrade);
            return Ok(page);
        }

        [HttpPost]
        [Route("api/grades")]
        public async Task<IHttpActionResult> AddLegacyGrade([FromBody] LegacyGradeBindingModel model)
        {
            
            model.ServiceTypeId = model.ServiceTypeId ?? 1; //wrtieen translation 
            
            var grade = AutoMapper.Mapper.Map<Grade>(model);
            
            await _repo.SaveLegacyGradeAndUpdateGradesAsync(grade, UserId);

            var result = await _repo.GetGraphs(x => x.Id == grade.Id).Select(GradeViewModel.FromGrade).FirstAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/grades/{id}")]
        public async Task<IHttpActionResult> UpdateLegacyGrade(int id, [FromBody] LegacyGradeBindingModel model)
        {
            model.ServiceTypeId = model.ServiceTypeId ?? 1; //wrtieen translation 
            
            var grade = AutoMapper.Mapper.Map<Grade>(model);

            await _repo.SaveLegacyGradeAndUpdateGradesAsync(grade, UserId);

            var result = await _repo.GetGraphs(x => x.Id == grade.Id).Select(GradeViewModel.FromGrade).FirstAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/grades/{id}")]
        public async Task<IHttpActionResult> DeleteGrade(int id)
        {
            await _repo.DeleteGradeAndUpdateGradesAsync(id, UserId);
            return Ok();
        }
        // TODO: rewrite !!!
        [HttpGet]
        [Route("api/serviceGrades/{languageId}")]
        public async Task<IHttpActionResult> Get(int languageId)
        {
            var parameters = _ctx.ServiceLanguageInfos
                .Where(x => x.Id == languageId)
                .Select(x => new {
                    LanguagePairId = x.LanguagePair.Id,
                    ProviderId = x.Service.ProviderId
                }).Single();

            var items = _ctx.Grades
                .Where(x => !x.IsDeleted)
                .Where(x => x.ProviderId == parameters.ProviderId && x.LanguagePair.Id == parameters.LanguagePairId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new {
                    id = x.Id,
                    // TODO: only legacy case is supported 
                    jobName = x.LegacyJobName,
                    primaryDomainName = x.PrimaryDomain.Name,
                    secondaryDomainName = x.SecondaryDomain.Name,
                    score = x.Score,
                    error = x.Error,
                    comment = x.Comment,
                    createdByName = x.CreatedBy.PersonName.FullName,
                    modifiedByName = x.ModifiedBy.PersonName.FullName,
                    createdDate = x.CreatedDate,
                    modifiedDate = x.ModifiedDate
                })
                .ToList();

            return Ok(items);
        }
    }
}
