using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class CustomerProfile
    {
        public int RoundingPolicyId { get; set; }
        public bool IsIndividual { get; set; }
        public string OrderLiteral { get; set; }

        public Person DefaultContactPerson { get; set; }
    }
}
