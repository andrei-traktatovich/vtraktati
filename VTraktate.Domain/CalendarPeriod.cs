using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class CalendarPeriod : ICalendarPeriod, ITimeStamped, IEntity
    {
        
        public int Id { get; set; }

        public string Comment { get; set; }

        public int StatusId { get; set; }
        public EmployeeCalendarStatus Status { get; set; }

        public int OfficeId { get; set; }
        public Office CurrentOffice { get; set; }

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

        public bool? Confirmed { get; set; }

        public int? ConfirmedById { get; set; }
        public Person ConfirmedBy { get; set; }
    }
}
