using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Repository.SnapshotProviders
{
    public class ProviderSnapshotBase
    {
        public virtual Provider Provider { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual Employment Employment { get; set; }
        public virtual CalendarPeriod EmployeeCalendarPeriod { get; set; }
        public virtual Freelance Freelance { get; set; }
        public virtual FreelanceCalendarPeriod FreelanceCalendarPeriod { get; set; }
        public virtual Service Service { get; set; }
        public virtual ServiceLanguageInfo Language { get; set; }
        public virtual ServiceDomainInfo Domain { get; set; }
    };
}
