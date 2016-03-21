using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Domain;
using VTraktate.DataAccess.ExtensionMethods;
using System.Data.Entity;

namespace VTraktate.Repository
{
    public class GlobalsProvider : IGlobalsProvider
    {
        private TraktatContext _ctx;
        public GlobalsProvider(TraktatContext ctx)
        {
            _ctx = ctx;
        }

        public AppGlobals Get()
        {
            // TODO: consider AutoMapper
            var employmentStatuses = _ctx.EmploymentStatuses
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .OrderBy(x => x.Id)
                .ToList();

            var freelanceStatuses = _ctx.FreelanceStatuses
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .OrderBy(x => x.Id)
                .ToList();

            var offices = _ctx.Offices
                .Include(x => x.Provider)
                .Select(x => new OfficeProfileModel { Id = x.Id, Title = x.Name, ProviderId = x.Provider != null ? x.Provider.Id : default(int?) })
                .OrderBy(x => x.Title)
                .ToList();

            var officeProviders = offices.Where(x => x.ProviderId.HasValue)
                .ToList();

            var providerTypes = _ctx.ProviderTypes
                .Select(x => new IdTitlePair { Id = (int)x.Id, Title = x.Name })
                .OrderBy(x => x.Id)
                .ToList();

            var titles = _ctx.Titles
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .OrderBy(x => x.Title)
                .ToList();
            
            var roles = _ctx.AspNetRoles
                .Select(x => new AppGlobals.RoleModel { Id = x.Id, Title = x.Name, Description = x.Description })
                .OrderBy(x => x.Title)
                .ToList();

            var languages = _ctx.LanguagePairs
                .OrderBy(x => x.FromRussian)
                .ThenBy(x => x.Name)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var availabilityStatuses = _ctx.ProviderAvailabilityStatuses
                .Select(x => new AppGlobals.AvailabilityStatusModel { Id = x.Id, Title = x.Name, Availability = x.Availability })
                .OrderBy(x => x.Id)
                .ToList();

            var serviceTypes = _ctx.ServiceTypes
                .Select(x => new VTraktate.Core.Interfaces.AppGlobals.ServiceTypeModel 
                { 
                    Id = x.Id, 
                    Title = x.Name,
                    RequiresDomain = x.SpecifyDomains,
                    RequiresLanguage = x.SpecifyLanguage
                })
                .OrderBy(x => x.Title)
                .ToList();

            var regions = _ctx.Regions
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .OrderBy(x => x.Id)
                .ToList();

            var currencies = _ctx.Currencies
                .OrderBy(x => x.Id)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var serviceUoms = _ctx.ServiceUOMs
                .OrderBy(x => x.Id)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var providerGroups = _ctx.ProviderGroups
                .OrderBy(x => x.Name)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var domains = _ctx.TranslationDomains
                .Where(x => x.Parent == null)
                .OrderBy(x => x.Name)
                .GroupJoin(_ctx.TranslationDomains.Where(x => x.ParentId != null).OrderBy(daughter => daughter.Name), 
                            parent => parent.Id, 
                            daughter => daughter.ParentId, 
                            (parent, daughters) => new { parent, daughters })
                .Select(x => new TranslationDomain[] { x.parent }.Concat(x.daughters))
                .SelectMany(x => x)
                .Select(x => new VTraktate.Core.Interfaces.AppGlobals.DomainModel { Id = x.Id, Title = x.Name, ParentId = x.ParentId, ParentName = x.Parent != null ? x.Parent.Name : "" })
                .ToList();


            var soft = _ctx.Software
                .OrderBy(x => x.Name)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var phoneTypes = _ctx.PhoneTypes
                .OrderBy(x => x.Name)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var providers = _ctx.Providers.Existing()        
                .OrderBy(x => x.Name)
                .Select(x => new IdNamePair { Id = x.Id, Name = x.Name })
                .ToList();

            var otherContactTypes = _ctx.OtherContactTypes
                .OrderBy(x => x.Name)
                .Select(x => new IdTitlePair { Id = x.Id, Title = x.Name })
                .ToList();

            var jobTypes = _ctx.JobTypes
                .OrderBy(x => x.Name)
                .Select(x => new VTraktate.Core.Interfaces.AppGlobals.JobTypeProfileModel 
                { 
                    Id = x.Id, 
                    Name = x.Name, 
                    LongName = x.LongName,
                    IsLinguistic = x.ServiceType.SpecifyLanguage,
                    IsInternal = x.IsInternal
                });

            var jobCompletionStatuses = _ctx.JobCompletionStatuses.OrderBy(x => x.Id).ToList();

            var result = new AppGlobals 
            { 
                EmploymentStatuses = employmentStatuses, 
                FreelanceStatuses = freelanceStatuses,
                Offices = offices,
                ProviderTypes = providerTypes, 
                Roles = roles, 
                Titles = titles,
                Languages = languages,
                AvailabilityStatuses = availabilityStatuses,
                Regions = regions, 
                ServiceTypes = serviceTypes,
                Currencies = currencies,
                ServiceUoms = serviceUoms,
                ProviderGroups = providerGroups,
                Domains = domains,
                Soft = soft,
                PhoneTypes = phoneTypes,
                Providers = providers,
                OtherContactTypes = otherContactTypes,
                JobTypes = jobTypes,
                JobCompletionStatuses = jobCompletionStatuses,
                OfficeProviders = officeProviders
            };
            return result;
        }
    }
}
