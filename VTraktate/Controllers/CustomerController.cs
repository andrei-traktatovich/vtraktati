using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using System.Data.Entity;
using VTraktate.Models;
using VTraktate.Core.Interfaces.BusinessLogic.Customers;

namespace VTraktate.Controllers
{
    public class CustomerController : ApiController
    {
        public CustomerController(ICustomerManager customerManager)
        {
            CustomerManager = customerManager;
        }

        protected ICustomerManager CustomerManager { get; private set; }

        [HttpGet]
        [Route("api/customer/{customerId}/profile")]
        public IHttpActionResult GetCustomerProfile(int customerId, [FromUri] int officeId)
        {
            // TODO: make this async
            var profile = CustomerManager.GetCustomerProfileAsync(customerId, officeId);
            var result = AutoMapper.Mapper.Map<CustomerProfileViewModel>(profile);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/customer/{id}/contacts")]
        public async Task<IHttpActionResult> GetContactPersons(int id, [FromUri] string search)
        {
            // ordered by full name !!! 
            var matches = await CustomerManager
                            .MatchContactsByNameOrContactInfo(id, search)
                            .OrderBy(x => x.PersonName.FullName)
                            .ToArrayAsync();

            var result = AutoMapper.Mapper.Map<CustomerContactViewModel[]>(matches);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/customer/autosuggest")]
        public async Task<IHttpActionResult> GetAutosuggestOptions([FromUri] string val)
        {
            try
            {
                // TODO: project instead of list&map
                var list = await CustomerManager.FindCustomersByName(val).ToListAsync();
                var options = AutoMapper.Mapper.Map<List<CustomerAutoSuggestModel>>(list);
                return Ok(options);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/contact/{id}")]
        public async Task<IHttpActionResult> UpdateCustomerContactPerson(int id, [FromBody] CustomerContactPersonEditBindingModel model)
        {
            throw new NotImplementedException();
            //await CustomerManager.UpdateContactPersonAsync(id, model);
            return Ok();
        }

    }
}