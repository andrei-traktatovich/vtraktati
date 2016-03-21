using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class OrderNumberComponents
    {
        public string CustomerCode { get; set; }
        public string OfficeCode { get; set; }
        public bool useOfficeCode { get; set; }
        public int? Number { get; set; }
        public string PostFix { get; set; }
    }
}
