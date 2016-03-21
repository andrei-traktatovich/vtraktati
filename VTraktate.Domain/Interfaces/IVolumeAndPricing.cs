using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;

namespace VTraktate.Domain.Interfaces
{
    public interface IVolumeAndPricing
    {
        JobVolumeAndPricing Initial { get; set; }
        JobVolumeAndPricing Final { get; set; }
    }
}
