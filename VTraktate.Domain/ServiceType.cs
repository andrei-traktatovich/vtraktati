using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTraktate.Domain
{
    public class ServiceType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool SpecifyLanguage { get; set; }
        public bool SpecifyDomains { get; set; }

        public int? LegacyId { get; set; }
    }
}
