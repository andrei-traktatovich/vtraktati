using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class UpdateProviderAddressBindingModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int RegionId { get; set; }
    }
}