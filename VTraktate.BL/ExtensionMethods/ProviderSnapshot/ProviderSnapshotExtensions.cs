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
            if (@this.CurrentCalendarPeriod != null)
                return @this.CurrentCalendarPeriod.OfficeId;
            if (@this.CurrentEmployment != null)
                return @this.CurrentEmployment.OfficeID;
            return null;
        }
    }
}
