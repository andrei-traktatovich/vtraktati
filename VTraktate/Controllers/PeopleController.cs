using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using System.Threading.Tasks;
using VTraktate.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace VTraktate.Controllers
{
    public class PeopleController : ApiController
    {
        
        public IRepo<Person> Repository { get; private set; }

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
        public PeopleController(IRepo<Person> repo)
        {
            Repository = repo;
        }

        

        [HttpPost]
        [Route("api/people/namecheck")]
        public async Task<IHttpActionResult> CheckUniquePersonName([FromBody] NamePreviewBindingModel model)
        {
            var name = model.Value.Trim();
            
            var exists = await Repository.Get().AnyAsync(x => x.PersonName.FullName == name
                && (!model.Id.HasValue || model.Id.Value != x.Id));
            
            var error = exists ? string.Format("Полное имя {0} не является уникальным.", name) : null;
            
            return Ok(new { Ok = !exists, ErrorMessage = error });
        }
    }
}
