using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public partial class AspNetRole
    {
        public const int ADMIN = 1;

        public static int[] RolesToDeleteProvider = new[] { ADMIN };
    }
}
