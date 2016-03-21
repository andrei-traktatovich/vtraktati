using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.BL
{
    public class EmploymentCalendarService : CalendarService<Employment>
    {
        private static Employment CloneEmployment(Employment source)
        {
            return new Employment
            {
                Comment = source.Comment,
                CreatedById = source.CreatedById,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                ModifiedById = source.ModifiedById,
                OfficeID = source.OfficeID,
                EndDate = source.EndDate,
                ModifiedDate = source.ModifiedDate,
                ProviderId = source.ProviderId,
                StatusID = source.StatusID,
                TitleId = source.TitleId,
                StartDate = source.StartDate
            };
        }
        public EmploymentCalendarService() : base(CalendarTimeScales.Day, CloneEmployment)
        {

        }
    }
}
