using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace VTraktate.Controllers
{
    public class EmailController : ApiController
    {
        private IRepo<Email> Repository;

        private int? _userId;
        protected int UserId 
        {
            get
            {
                if (_userId == null)
                    _userId = User.Identity.GetUserId<int>();
                return _userId.Value;
            }
        }

        public EmailController(IRepo<Email> emailRepo)
        {
            Repository = emailRepo;
        }
        
        [HttpPost]
        [Route("api/people/{id}/emails")]
        public async Task<IHttpActionResult> PostEmail(int id, [FromBody] EmailBindingModel email)
        {
            var newEmail = Mapper.Map<Email>(email);
            newEmail.ContactPersonId = id;
            Repository.AddOrUpdate(newEmail);
            await Repository.SaveAsUserAsync(UserId);

            var newlyAdded = await Repository.Get(x => x.Id == newEmail.Id).FirstOrDefaultAsync();
            if (newlyAdded != null)
            {
                var model = Mapper.Map<EmailViewModel>(newlyAdded);
                return Ok(model);
            }
            else
                return InternalServerError();
        }

        [Route("api/emails/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await Repository.DeleteAsync(id);
            await Repository.SaveAsUserAsync(UserId);
            return Ok();
        }

        [Route("api/emails/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] EmailBindingModel email)
        {
            var existing = await Repository.GetGraphs(x => x.Id == id).FirstOrDefaultAsync();
            Mapper.Map<EmailBindingModel, Email>(email, existing);
            await Repository.SaveAsUserAsync(UserId);
            var modified = Mapper.Map<EmailViewModel>(existing);
            return Ok(modified);
        }
    }
}
