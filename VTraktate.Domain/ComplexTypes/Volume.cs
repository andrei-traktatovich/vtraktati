using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.ComplexTypes
{
    [ComplexType]
    public class Volume
    {
        public decimal? Pages { get; set; }
        public int? Chars { get; set; }
        public int? Words { get; set; }
    }
}
