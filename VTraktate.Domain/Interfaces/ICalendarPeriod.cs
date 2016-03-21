using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.Interfaces
{
    public interface ICalendarPeriod
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}
