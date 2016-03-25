using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain
{
    public static class EntityUtils
    {
        public static bool IsNull(object obj)
        {
            return obj == null;
        }
    }
}
