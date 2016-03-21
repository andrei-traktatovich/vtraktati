using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Employment : ICalendarPeriod, ITimeStamped, ISoftDelete, IEntity
    {
        
        public int Id { get; set; }

        public int OfficeID { get; set; }
        public Office Office { get; set; }

        public int TitleId { get; set; }
        public Title  Title { get; set; }

        public string Comment { get; set; }

        public int StatusID { get; set; }
        public EmploymentStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        

        public bool IsDeleted { get;set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        [ForeignKey("CreatedBy")]
        public int? CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        [ForeignKey("ModifiedBy")]
        public int? ModifiedById { get; set; }
        public Person ModifiedBy { get; set; }

        public virtual ICollection<CalendarPeriod> CalendarPeriods { get; set; }
    }
}
