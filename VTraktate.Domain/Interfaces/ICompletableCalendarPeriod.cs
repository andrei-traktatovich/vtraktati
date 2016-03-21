using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.Interfaces
{
    public interface ICompletableCalendarPeriod : ICalendarPeriod
    {
        DateTime? CompletionDate { get; set; }
        
    }
    
}

