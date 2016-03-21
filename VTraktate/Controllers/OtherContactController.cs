using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;

namespace VTraktate.Controllers
{

    public class OtherContactController : ApiController
    {
        private IRepo<OtherContact> Repository;

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

        public OtherContactController(IRepo<OtherContact> phoneRepo)
        {
            Repository = phoneRepo;
        }

        [HttpPost]
        [Route("api/people/{id}/otherContact")]
        public async Task<IHttpActionResult> PostOtherContact(int id, [FromBody] ProviderProfileOtherContactBindingModel contact)
        {
            var newContact = Mapper.Map<OtherContact>(contact);
            newContact.PersonId = id;
            Repository.AddOrUpdate(newContact);
            await Repository.SaveAsUserAsync(UserId);

            var newlyAdded = await Repository.Get(x => x.Id == newContact.Id).FirstOrDefaultAsync();
            if (newlyAdded != null)
            {
                var model = Mapper.Map<OtherContactViewModel>(newlyAdded);
                return Ok(model);
            }
            else
                return InternalServerError();
        }
        [HttpDelete]
        [Route("api/otherContact/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await Repository.DeleteAsync(id);
            await Repository.SaveAsUserAsync(UserId);
            return Ok();
        }

        [Route("api/otherContact/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] ProviderProfileOtherContactBindingModel otherContact)
        {
            var existing = await Repository.GetGraphs(x => x.Id == id).FirstOrDefaultAsync();
            Mapper.Map<ProviderProfileOtherContactBindingModel, OtherContact>(otherContact, existing);
            
            existing.IsDeleted = false;

            await Repository.SaveAsUserAsync(UserId);
            var modified = Mapper.Map<OtherContactViewModel>(existing);
            return Ok(modified);
        }
    }
}

