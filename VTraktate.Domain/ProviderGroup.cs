using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class ProviderGroup : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }
    }
}
