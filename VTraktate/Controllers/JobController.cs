using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using VTraktate.Core.Interfaces.BusinessLogic.Orders.JobParts;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain;
using VTraktate.Models.Order;
using VTraktate.Models.Order.JobPart;
using VTraktate.Models.Order.ManagerOrderGrid;

namespace VTraktate.Controllers
{
    public class JobController : AuthenticatedControllerBase
    {
        public JobController(IJobManager jobManager, IQueryFilterService<Job> jobFilterService, IJobPartManager jobPartManager)
        {
            this._jobManager = jobManager;
            this._filterService = jobFilterService;
            this._jobPartManager = jobPartManager;
        }

        private IJobManager _jobManager;
        private IQueryFilterService<Job> _filterService;
        private IJobPartManager _jobPartManager;

        [HttpGet]
        [Route("api/job")]
        public async Task<IHttpActionResult> Get([FromUri] JobFilterBindingModel filterModel)
        {
            IQueryable<Job> jobs = _jobManager.GetGraphs();
            // TODO: accomodate automapper projections into transform  ?? 
            var interim = await _filterService.GetFilteredOrderedPageAsync<Job>(
                jobs, filterModel.Filter, filterModel.Sort, filterModel.Page, filterModel.Count,
                item => item, true);
            var mapped = AutoMapper.Mapper.Map<List<OrderGridViewModel>>(interim.Result);
            return Ok(new { Result = mapped, Total = interim.Total });
        }

        [HttpPut]
        [Route("api/job/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] JobEditBindingModel model)
        {
            // check model sanity
            var jobs =  _jobManager.GetGraphs(x => x.Id == id);
            var job = jobs.SingleOrDefault();
            if (job == null)
                return BadRequest(string.Format("Ошибка: Заказ с Id = {0} не найден.", id));
            AutoMapper.Mapper.Map<JobEditBindingModel, Job>(model, job);
            await _jobManager.UpdateAndSave(job, UserId);
            return Ok();
        }
        
        [HttpPost]
        [Route("api/order/{id}/append")]
        public async Task<IHttpActionResult> AppendJob(int id, [FromBody] JobEditBindingModel model)
        {
            var job = AutoMapper.Mapper.Map<Job>(model);
            await _jobManager.UpdateAndSave(job, UserId);
            return Ok();
        }

        [HttpPut]
        [Route("api/job/{id}/status/{statusId}")]
        public async Task<IHttpActionResult> SetStatus(int id, int statusId) // also optionally pass in body new final volume?? 
        {
            var jobs = _jobManager.GetGraphs(x => x.Id == id);
            var job = jobs.SingleOrDefault();
            if (job == null)
                return BadRequest(string.Format("Ошибка: Заказ с Id = {0} не найден.", id));
            job.StatusId = statusId;
            await _jobManager.UpdateAndSave(job, UserId);
            return Ok();
        }

        [HttpPost]
        [Route("api/job/{jobId}/jobPart")]
        public async Task<IHttpActionResult> AddParticipant(int jobId, [FromBody] JobPartCreateBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Сохранение невозможно: неверные или неполные данные.");
            var jobPart = AutoMapper.Mapper.Map<JobPart>(model);
            var result = await _jobPartManager.CreateJobPartAsync(jobId, jobPart, UserId);
            
            //var viewModel = AutoMapper.Mapper.Map<OrderGridJobPartViewModel>(result);
            //return Ok(viewModel);
            return Ok();
        }

        [HttpDelete]
        [Route("api/jobPart/{id}")]
        public async Task<IHttpActionResult> DeleteParticipant(int id)
        {
            await _jobPartManager.DeleteAsync(id, UserId);
            return Ok();
        }

        [HttpPut]
        [Route("api/jobPart/{jobPartId}/status/{statusId}")]
        public async Task<IHttpActionResult> ChangeParticipantStatus(int jobPartId, int statusId)
        {
            await _jobPartManager.ModifyJobPartStatusAsync(jobPartId, statusId, UserId);
            return Ok();
        }

        [HttpPut]
        [Route("api/jobPart/{jobPartId}")]
        public async Task<IHttpActionResult> UpdateParticipant(int jobPartId, [FromBody] JobPartEditBindingModel model)
        {
            var jobPart = _jobPartManager.GetById(jobPartId);

            AutoMapper.Mapper.Map<JobPartEditBindingModel, JobPart>(model, jobPart);
            await _jobPartManager.UpdateJobPartAsync(jobPart, UserId);
            // not sending any data for now
            return Ok();
        }

        [HttpDelete]
        [Route("api/job/{id}")]
        public async Task<IHttpActionResult> DeleteJob(int id) 
        {
            await _jobManager.DeleteAsync(id, UserId);
            return Ok();
        }
    }
}