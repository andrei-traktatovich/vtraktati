﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces;

namespace VTraktate.BL.Cerberos
{
    public class CerberosMum : ICerberosMum
    {
        public ICerberos MakeCerberos(ITraktatContext context, Func<int> userIdFunc)
        {
            return new Cerberos(context, userIdFunc);
        }
    }
}
