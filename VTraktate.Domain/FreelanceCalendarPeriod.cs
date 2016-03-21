using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class FreelanceCalendarPeriod : ICalendarPeriod, ITimeStamped, ISoftDelete, IEntity
    {

        public int Id { get; set; }

        public string Comment { get; set; }

        public int StatusId { get; set; }
        public ProviderAvailabilityStatus Status { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public Person ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
