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
    
        public class PhoneController : ApiController
        {
            private IRepo<Phone> Repository; 

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

            public PhoneController(IRepo<Phone> phoneRepo)
            {
                Repository = phoneRepo;
            }

            [HttpPost]
            [Route("api/people/{id}/phones")]
            public async Task<IHttpActionResult> PostPhone(int id, [FromBody] PhoneBindingModel phone)
            {
                var newPhone = Mapper.Map<Phone>(phone);
                newPhone.ContactPersonId = id;
                Repository.AddOrUpdate(newPhone);
                await Repository.SaveAsUserAsync(UserId);

                var newlyAdded = await Repository.Get(x => x.Id == newPhone.Id).FirstOrDefaultAsync();
                if (newlyAdded != null)
                {
                    var model = Mapper.Map<PhoneViewModel>(newlyAdded);
                    return Ok(model);
                }
                else
                    return InternalServerError();
            }

            [Route("api/phones/{id}")]
            public async Task<IHttpActionResult> Delete(int id)
            {
                await Repository.DeleteAsync(id);
                await Repository.SaveAsUserAsync(UserId);
                return Ok();
            }

            [Route("api/phones/{id}")]
            public async Task<IHttpActionResult> Put(int id, [FromBody] PhoneBindingModel phone)
            {
                var existing = await Repository.GetGraphs(x => x.Id == id).FirstOrDefaultAsync();
                Mapper.Map<PhoneBindingModel, Phone>(phone, existing);
                await Repository.SaveAsUserAsync(UserId);
                var modified = Mapper.Map<PhoneViewModel>(existing);
                return Ok(modified);
            }
        }
    }

