using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Promotion : ICalendarPeriod, IEntity, ITimeStamped
    {
        public int Id { get; set; }

        public int PromoteeId { get; set; }
        public Provider Promotee { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set;}

        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }
        public Person CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }
        public Person ModifiedBy { get; set; }
        
    }
}
