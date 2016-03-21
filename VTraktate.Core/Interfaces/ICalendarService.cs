using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Core.Interfaces
{
    public interface ICalendarService<T>
        where T: ICalendarPeriod, ISoftDelete
    {
        IEnumerable<T> Insert(IList<T> periods, T newPeriod);
            
    }
}
