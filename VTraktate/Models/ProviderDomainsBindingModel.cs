using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ProviderDomainsBindingModel
    {
        public int Stars { get; set; }
        public string Comment { get; set; }
        public IEnumerable<int> DomainIds { get; set; }

    }
}