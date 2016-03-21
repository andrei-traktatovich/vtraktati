using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.BL
{
    public class FreelanceCalendarService : CalendarService<Freelance>
    {
        private static Func<Freelance, Freelance> CloneFreelance = x => new Freelance
        {
            Comment = x.Comment,
            CreatedById = x.CreatedById,
            CreatedDate = x.CreatedDate,
            EndDate = x.EndDate,
            FreelanceStatusID = x.FreelanceStatusID,
            IsDeleted = x.IsDeleted,
            ModifiedById = x.ModifiedById,
            ProviderID = x.ProviderID,
            StartDate = x.StartDate
        };
        public FreelanceCalendarService()
            : base(CalendarTimeScales.Day, CloneFreelance)
        {

        }
    }
}
