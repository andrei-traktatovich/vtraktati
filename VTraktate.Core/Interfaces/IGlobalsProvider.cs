using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Infrastructure;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces
{
    public interface IGlobalsProvider
    {
        AppGlobals Get();
    }
    public class OfficeProfileModel : IdTitlePair
    {
        public int? ProviderId { get; set; }
    }
    
    public class AppGlobals
    {
        public class RoleModel : IdTitlePair
        {
            public string Description { get; set; }
        }

        public class AvailabilityStatusModel : IdTitlePair
        {
            public bool Availability { get; set; }
        }

        public class DomainModel : IdTitlePair
        {
            public int? ParentId { get; set; }

            public string ParentName { get; set; }
        }

        public class ServiceTypeModel : IdTitlePair
        {
            public bool RequiresLanguage { get; set; }
            public bool RequiresDomain { get; set; }
        }

        public class JobTypeProfileModel : IdNamePair
        {
            public bool IsLinguistic { get; set; }
            public string LongName {get;set;}
            public bool IsInternal { get; set; }
        }

        

        public IEnumerable<RoleModel> Roles { get; set; }
        public IEnumerable<IdTitlePair> ProviderTypes { get; set; }
        public IEnumerable<OfficeProfileModel> Offices { get; set; } // TODO : this will probably be a separate class 
        public IEnumerable<IdTitlePair> Titles { get; set; }
        public IEnumerable<IdTitlePair> EmploymentStatuses { get; set; }
        public IEnumerable<IdTitlePair> FreelanceStatuses { get; set; }

        public IEnumerable<ServiceTypeModel> ServiceTypes { get; set; }

        public IEnumerable<AvailabilityStatusModel> AvailabilityStatuses { get; set; }

        public IEnumerable<IdTitlePair> Languages { get; set; }
        public IEnumerable<IdTitlePair> Regions { get; set; }

        public IEnumerable<IdTitlePair> Currencies { get; set; }

        public IEnumerable<IdTitlePair> ServiceUoms { get; set; }

        public IEnumerable<IdTitlePair> ProviderGroups { get; set; }

        public IEnumerable<DomainModel> Domains { get; set; }

        public IEnumerable<IdTitlePair> Soft { get; set; }

        public IEnumerable<IdTitlePair> PhoneTypes { get; set; }

        public IEnumerable<IdNamePair> Providers { get; set; }

        public IEnumerable<IdTitlePair> OtherContactTypes { get; set; }

        public IEnumerable<JobTypeProfileModel> JobTypes { get; set; }

        public IEnumerable<JobCompletionStatus> JobCompletionStatuses { get; set; }

        public IEnumerable<OfficeProfileModel> OfficeProviders { get; set; }

        public IEnumerable<IdNamePair> LegalForms { get; set; }
    }
}