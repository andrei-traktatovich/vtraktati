using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class CustomerProfileViewModel
    {
        public CustomerContactViewModel DefaultContactPerson { get; set; }
        public int RoundingPolicyId { get; set; }
        public bool IsIndividual { get; set; }
        public string OrderLiteral { get; set; }
    }
}