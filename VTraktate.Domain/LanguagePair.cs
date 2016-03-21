using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTraktate.Domain
{
    public class LanguagePair : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool FromRussian { get; set; }
        public int? LegacyId { get; set;}
    }
}
