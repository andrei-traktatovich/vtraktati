using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.Snapshots
{
    public class ExtendedProviderSnapshot
    {
        public Provider Provider { get; set; }

        public Employment Employment { get; set; }

        public Freelance Freelance { get; set; }

        public CalendarPeriod EmployeeCalendarPeriod { get; set; }

        public FreelanceCalendarPeriod FreelanceCalendarPeriod { get; set; }

        public Promotion Promotion { get; set; }

        public Service Service { get; set; }

        public ServiceLanguageInfo Language { get; set; }


        public bool IsBusy { get; set; }

        public DateTime? BusyThrough { get; set; }

        public IEnumerable<JobPart> PendingJobParts { get; set; }

        public int AvailabilityStatusId { get; set; }
    }

    
}
