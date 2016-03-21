using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public class ProviderType  
    {
        public ProviderTypes Id { get; set; }
        public string Name { get; set; }
    }

    public enum ProviderTypes
    {
        Inhouse = 0,
        Freelance = 1,
        Office = 2,
        Contractor = 3
    }
}
