using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.BL.ExtensionMethods.ProviderSnapshotExtensions
{
    public static class ProviderSnapshotExtensions
    {
        public static int? GetCurrentOfficeId(this ProviderSnapshot @this)
        {
            return @this.CurrentCalendarPeriod?.OfficeId ?? @this.CurrentEmployment?.OfficeID;
        }
    }
}
