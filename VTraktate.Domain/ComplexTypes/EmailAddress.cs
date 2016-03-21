using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.ComplexTypes
{
    public class EmailAddress
    {
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
    }
}
