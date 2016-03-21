using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;

namespace VTraktate.Domain.Interfaces
{
    public interface ITimeStamped
    {
        DateTime CreatedDate { get; set; }
        int? CreatedById { get; set; }
        Person CreatedBy { get; set; }
        
        DateTime? ModifiedDate { get; set; }
        
        int? ModifiedById { get; set; }
        Person ModifiedBy { get; set; }
    }
}
