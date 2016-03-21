using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTraktate.Domain
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<JobPart> JobParts { get; set; }
    }
}
