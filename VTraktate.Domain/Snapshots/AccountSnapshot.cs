using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.Snapshots
{
    public class AccountSnapshot
    {
        public Person Person { get; set; }

        public AspNetUser AspNetUser { get; set; }
        public ProviderSnapshot ProviderSnapShot { get; set; }
    }
}
