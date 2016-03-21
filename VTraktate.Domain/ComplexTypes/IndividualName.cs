using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.ComplexTypes
{
    public class IndividualName
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string AlternateName { get; set; }
        public string AddressName { get; set; }
        public string Initials { get; set; }

    }
}
