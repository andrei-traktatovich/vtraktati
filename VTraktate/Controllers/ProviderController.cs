using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Repository.SnapshotProviders;
using System.Data.Entity;
using VTraktate.Core.Interfaces.Filtering;
using VTraktate.Domain.Snapshots;
using VTraktate.Models;
using VTraktate.Validation;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using AutoMapper.QueryableExtensions;
using VTraktate.Domain.ComplexTypes;
using Microsoft.AspNet.Identity;
using AutoMapper;
using VTraktate.Core.Interfaces;

namespace VTraktate.Controllers
{
    [Authorize]
    public class ProviderController : ApiController
    {
        private IQueryFilterService<ExtendedProviderSnapshot> _queryFilterService;
        private ISnapshotProvider<ExtendedProviderSnapshot> _extendedSnapshotProvider;
        private IRepo<Provider> _providerRepo;
        private Validator _validator;
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        protected ICalendarService<Freelance> FreelanceCalendarService { get; private set; }
        protected ICalendarService<Employment> EmploymentCalendarService { get; private set; }
        protected ICalendarService<FreelanceCalendarPeriod> AvailabilityCalendarService { get; private set; }

        public ProviderController(ISnapshotProvider<ExtendedProviderSnapshot> extendedSnapshotProvider, 
            IQueryFilterService<ExtendedProviderSnapshot> queryFilterService, 
            Validator validator,
            IRepo<Provider> providerRepo,
            ICalendarService<Freelance> calendarService,
            ICalendarService<Employment> employmentCalendarService,
            ICalendarService<FreelanceCalendarPeriod> availabilityCalendarService
            )
        {
            _extendedSnapshotProvider = extendedSnapshotProvider;
            _queryFilterService = queryFilterService;
            _validator = validator;
            _providerRepo = providerRepo;
            FreelanceCalendarService = calendarService;
            EmploymentCalendarService = employmentCalendarService;
            AvailabilityCalendarService = availabilityCalendarService;
        }


        [HttpGet]
        [Route("api/provider/{id}/appointProfile")]
        public async Task<IHttpActionResult> GetAppointmentProfile(int id, [FromUri] int jobTypeId, [FromUri] int? languageId = null)
        {
            var service = _providerRepo.GetAny<JobType>(x => x.Id == jobTypeId).Select(x => x.ServiceType).FirstOrDefault();
            // throw if service is bad

            var profile = _providerRepo.GetGraphs(x => x.Id == id).SingleOrDefault();
             
            if (profile == null)
                throw new InvalidOperationException();

            
            var serviceItem = profile.Services.Where(x => x.ServiceTypeId == service.Id).SingleOrDefault();
            if (serviceItem == null)
                throw new InvalidOperationException();
            if (service.SpecifyLanguage)
            {
                var language = serviceItem.Languages.Where(x => x.LanguagePairId == languageId).SingleOrDefault();
                if (language == null)
                    throw new InvalidOperationException();

                return Ok(new 
                {
                    currencyId = serviceItem.CurrencyId,
                    uomId = serviceItem.ServiceUOMId,
                    minRate = language.Rate.Minrate,
                    maxrate = language.Rate.MaxRate 
                });
            }
            else
                return Ok(new 
                {
                    currencyId = serviceItem.CurrencyId,
                    uomId = serviceItem.ServiceUOMId,
                    minRate = serviceItem.Rate.Minrate,
                    maxrate = serviceItem.Rate.MaxRate 
                });
        }

        [HttpGet]
        [Route("api/provider/autosuggest")]
        public async Task<IHttpActionResult> GetAutosuggestOptions(
            [FromUri] string search = null, 
            [FromUri] int? jobTypeId = null, 
            [FromUri] int? languageId = null, 
            [FromUri] int? domain1Id = null, 
            [FromUri] int? domain2Id = null,
            [FromUri] int? officeId = null)
        {
            var query = _extendedSnapshotProvider.Get();
            try
            {
                var showRateAndQA = false;
                int  serviceTypeId = 0;
                var filter = new ProviderFilterBindingModelFilter();
                var service = _providerRepo.GetAny<JobType>(x => x.Id == jobTypeId.Value).Select(x => x.ServiceType).FirstOrDefault();
                if (service == null)
                    showRateAndQA = false;
                else
                {
                    if (service.SpecifyLanguage && languageId.HasValue || !service.SpecifyLanguage)
                        showRateAndQA = true;
                    serviceTypeId = service.Id;
                    filter.Name = search;
                    filter.ServiceTypes = service != null ? new List<int> { serviceTypeId } : null;
                    filter.Languages = languageId.HasValue ? new List<int> { languageId.Value } : null;
                    // domains ... 
                };
                if (officeId != null)
                {
                    filter.Offices = new List<int> { officeId.Value };
                    filter.EmplStatuses = new List<int> { 1, 2, 3, 4, 5 };
                }
            

                var result = await _queryFilterService.GetFilteredOrderedPageAsync<ProvidersViewModel>(
                    query,
                    filter,
                    new ProviderFilterBindingModelSorting(),
                    1,
                    25,
                    ProvidersViewModel.FromExtendedProviderSnapshot(showRateAndQA), 
                    useDistinctAfterProjection: true, 
                    ignoreTerminalFilters: true);

                return Ok<object>(new { showRateAndQA = showRateAndQA, result = result });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public async Task<IHttpActionResult> Get([FromUri] ProviderFilterBindingModel search)
        {

            var query = _extendedSnapshotProvider.Get();
            
            try
            {
                var showRateAndQA = (search != null && ShowRateAndQA(search));
                
                var result = await _queryFilterService.GetFilteredOrderedPageAsync<ProvidersViewModel>(
                    query, 
                    search.Filter, 
                    search.Sorting, 
                    search.Page, 
                    search.Count, 
                    ProvidersViewModel.FromExtendedProviderSnapshot(showRateAndQA));

                return Ok<object>(new { showRateAndQA = showRateAndQA, result = result });
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private bool ShowRateAndQA(ProviderFilterBindingModel model)
        {
            if (model.Filter == null || model.Filter.ServiceTypes == null || model.Filter.ServiceTypes.Count() != 1)
                return false;

            if (!string.IsNullOrEmpty(model.Filter.Name))
                return false;
            
            var serviceId = model.Filter.ServiceTypes.First();

            if (!_validator.IsLinguisticServiceType(serviceId)) // get all service types, find one that matches id, return it's RequiresLanguages property ... 
                return true;

            return model.Filter.Languages != null && model.Filter.Languages.Count() == 1;  
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var query = _providerRepo.GetGraphs(x => x.Id == id);
                
            // OPTIMIZE: not efficient because makes lots of rountrips. Needs to use the Project() extension method
            // but using project() I get CANNOT COMPARE COMPLEX TYPE "QA"... etc.
            var provider = await query.FirstOrDefaultAsync();

            if (provider != null)
            {
                var result = Mapper.Map<ProviderProfileViewModel>(provider);
                return Ok(result);
            }
            else
                return BadRequest(String.Format("Данные не найдены, Id = {0}", id));
        }

        [Route("api/provider/{providerId}/address")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAddress(int providerId, [FromBody] UpdateProviderAddressBindingModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            var provider = await _providerRepo.Get(x => x.Id == providerId).SingleOrDefaultAsync();

            if (provider == null)
                return BadRequest(String.Format("Провайдер с ID = {0} не найден", providerId));

            provider.City = model.City;
            provider.Address = model.Address;
            provider.RegionId = model.RegionId;

            provider.LegalFormId = model.LegalFormId;
            provider.WorksNightly = model.WorksNightly;
            provider.TimeDifference = model.TimeDifference;

            await _providerRepo.SaveAsUserAsync(userId);
            return Ok();
        }

        [Route("api/provider/individual")]
        public async Task<IHttpActionResult> Post([FromBody] IndividualProviderModel model)
        {
            // REFACTOR ::: this should be delegated to a separate class ...
            
            var userId = User.Identity.GetUserId<int>();

            var now = DateTime.Now;
            var date = now.Date;

            // TEMPORARY 
            
            if (String.IsNullOrEmpty(model.PersonName.FullName.Trim()))
                return BadRequest("Пустое имя исполнителя");
            
            var provider = new Provider { 
                Name = model.PersonName.FullName, 
                ProviderTypeId = (ProviderTypes)model.Type.Id,
                RegionId = model.Details.Regionid,
                City = model.Details.City,
                Address = model.Details.Address,
                LegalFormId = model.Details.LegalFormId,
                TimeDifference = model.Details.TimeDifference,
                WorksNightly = model.Details.WorksNightly,
                Services = Mapper.Map<IEnumerable<ServiceBindingModel>, ICollection<Service>>(model.Services),
            };

            
            if (model.Freelance != null)
            {
                var freelance = new Freelance { FreelanceStatusID = model.Freelance.StatusId, Comment = model.Freelance.Comment, StartDate = DateTime.Today };
                provider.Freelances = new List<Freelance> { freelance };
            }

            if (provider.ProviderTypeId == ProviderTypes.Inhouse)
            {
                var employment = AutoMapper.Mapper.Map<EmploymentBindingModel, Employment>(model.Employment);
                if (employment != null)
                    provider.Employments = new List<Employment> { employment };
            }
            // Add Promotion 

            if (model.Promote)
            {
                provider.Promotions = new List<Promotion> { 
                    new Promotion { 
                        StartDate = date, 
                        EndDate = date.AddMonths(4),
                        PromotedById = userId, 
                        Promotee = provider 
                    }
                };
            }

            var providerAsContactPerson = new Person
            {
                BirthDate = model.Details.BirthDay,
                Comment = model.Details.Comment,
                CreatedById = userId,
                CreatedDate = now,
                ModifiedById = userId,
                ModifiedDate = now,
                PersonName = new IndividualName
                {
                    AddressName = model.PersonName.Address,
                    AlternateName = model.PersonName.AlternateName,
                    
                    FullName = model.PersonName.FullName,
                    Initials = model.PersonName.Initials,
                    
                    LastName = model.PersonName.Name.LastName,
                    FirstName = model.PersonName.Name.FirstName,
                    MiddleName = model.PersonName.Name.MiddleName
                },
                Emails = Mapper.Map<IEnumerable<EmailBindingModel>, ICollection<Email>>(model.Emails),
                Phones = Mapper.Map<IEnumerable<PhoneBindingModel>, ICollection<Phone>>(model.Telephones),
                OtherContacts = Mapper.Map<IEnumerable<OtherContactsBindingModel>, ICollection<OtherContact>>(model.OtherContacts)
                // TODO: add other stuff 
            };

            provider.ContactPersons = new List<Person> { providerAsContactPerson };

            this._providerRepo.AddOrUpdate(provider);
            await this._providerRepo.SaveAsUserAsync(userId);

            return Ok();
        }
        
        
        [Route("api/provider/{id}/freelance")]
        public async Task<IHttpActionResult> PostFreelance(int id, [FromBody] FreelanceBindingModel model)
        {
            var provider = await _providerRepo.FindByIdAsync(id);
            if (provider == null)
            {
                return BadRequest("provider not found");
            }

            
            await MakeFreelance(provider, model, DateTime.Today);
            
            return Ok();
        }

        [Route("api/provider/{id}/employment")]
        public async Task<IHttpActionResult> PostEmployment(int id, [FromBody] EmploymentBindingModel model)
        {
            var provider = await _providerRepo.FindByIdAsync(id);
            
            if(provider == null)
            {
                return BadRequest("provider not found");
            }

            await MakeEmployment(provider, model);
            
            return Ok();
        }

        private async Task MakeEmployment(Provider provider, EmploymentBindingModel model)
        {
            var newEmployment = Mapper.Map<Employment>(model);

            EmploymentCalendarService.Insert(provider.Employments.ToList(), newEmployment);

            provider.Employments.Add(newEmployment);

            await _providerRepo.SaveAsUserAsync(UserId);
        }

        private async Task MakeFreelance(Provider provider, FreelanceBindingModel model, DateTime startDate)
        {
            var newFreelance = Mapper.Map<Freelance>(model);

            newFreelance.StartDate = startDate;

            FreelanceCalendarService.Insert(provider.Freelances.ToList(), newFreelance);

            provider.Freelances.Add(newFreelance);

            await _providerRepo.SaveAsUserAsync(UserId);
        }


        [HttpPost]
        [Route("api/provider/{id}/hire")]
        public async Task<IHttpActionResult> Hire(int id, [FromBody] EmploymentBindingModel model)
        {
            var provider = await _providerRepo.FindByIdAsync(id);
            
            if (provider == null)
            {
                return BadRequest("provider not found");
            }

            provider.ProviderTypeId = ProviderTypes.Inhouse;
            
            await MakeEmployment(provider, model);

            return Ok();
        }

        [HttpPost]
        [Route("api/provider/{id}/fire")]
        public async Task<IHttpActionResult> Fire(int id, [FromBody] FireEmployeeBindingModel model)
        {
            var provider = await _providerRepo.FindByIdAsync(id);

            if (provider == null)
            {
                return BadRequest("provider not found");
            }

            provider.ProviderTypeId = ProviderTypes.Freelance;
            model.LastEmployment.StatusId = 6; // ATTN : MAGIC NUMBER !!!! 
            
            await MakeEmployment(provider, model.LastEmployment);

            await MakeFreelance(provider, model.Freelance, model.LastEmployment.StartDate);

            return Ok();
        }

        [HttpGet]
        [Route("api/provider/{id}/availability")]
        public async Task<IHttpActionResult> GetAvailability(int id)
        {
            var provider = await _providerRepo.GetGraphs(x => x.Id == id).FirstOrDefaultAsync();

            if (provider == null)
            {
                return BadRequest("provider not found");
            }
            
            // TODO: need to apply EXISTING here !!! 
            var freelanceCalendarPeriods = provider.FreelanceCalendarPeriods.Where(x => !x.IsDeleted).ToList();

            var result = new 
            {
                AvailabilityStatuses = Mapper.Map<IEnumerable<FreelanceCalendarPeriodViewModel>>(freelanceCalendarPeriods)
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("api/provider/{id}/availability")]
        public async Task<IHttpActionResult> PostAvailability(int id, [FromBody]FreelanceCalendarPeriodBindingModel model)
        {
            var provider = await _providerRepo.GetGraphs(x => x.Id == id).FirstOrDefaultAsync();

            var freelanceCalendarPeriods = provider.FreelanceCalendarPeriods.ToList();
            
            var newPeriod = Mapper.Map<FreelanceCalendarPeriod>(model);
            
            newPeriod.ProviderId = provider.Id; 
            
            var newPeriods = AvailabilityCalendarService.Insert(freelanceCalendarPeriods, newPeriod);
            foreach(var item in newPeriods)
            {
                provider.FreelanceCalendarPeriods.Add(item);
            }
            await _providerRepo.SaveAsUserAsync(UserId);

            return Ok(Mapper.Map<FreelanceCalendarPeriodViewModel>(newPeriod));
        }
    }
}
