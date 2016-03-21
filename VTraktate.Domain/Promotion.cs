using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain
{
    public class Promotion : ICalendarPeriod, IEntity
    {
        public int Id { get; set; }

        public int PromoteeId { get; set; }
        public Provider Promotee { get; set; }
        public int PromotedById { get; set; }
        public Person PromotedBy { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set;}
    }
}
