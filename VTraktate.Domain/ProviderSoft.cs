using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class ProviderSoft : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Provider> Providers { get; set; }
    }
}
