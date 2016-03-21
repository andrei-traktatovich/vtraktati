using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.BL
{
    public class AvailabilityCalendarService : CalendarService<FreelanceCalendarPeriod>
    {
        public static Func<FreelanceCalendarPeriod, FreelanceCalendarPeriod> Cloner = x => new FreelanceCalendarPeriod
        {
            Comment = x.Comment,
            CreatedById = x.CreatedById,
            EndDate = x.EndDate,
            CreatedDate = x.CreatedDate,
            IsDeleted = x.IsDeleted,
            ModifiedById = x.ModifiedById,
            ModifiedDate = x.ModifiedDate,
            ProviderId = x.ProviderId,
            StartDate = x.StartDate,
            StatusId = x.StatusId
        };
        public AvailabilityCalendarService() : base(CalendarTimeScales.Day, Cloner)
        {

        }

        public override void RoundDateTimes(FreelanceCalendarPeriod period)
        {
            base.RoundDateTimes(period);
            if (period.EndDate.HasValue)
                period.EndDate = period.EndDate.Value.AddDays(1).AddMinutes(-1);
        }

        // override functions to maniputlate times such as 00:00, 23.59 etc. ... ok? 
    }
}
