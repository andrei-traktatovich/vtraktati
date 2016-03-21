using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain.ComplexTypes;
using VTraktate.Domain.Interfaces;

namespace VTraktate.Domain.Extensions
{
    public static class IVolumeAndPricingExtension
    {
        public static void EnsureNonNullFinalVolumeAndPricing(this IVolumeAndPricing @this)
        {
            if (@this.Final == null)
                @this.Final = new JobVolumeAndPricing
                {
                    Pricing = new JobPricing(),
                    Volume = new Volume()
                };
            else
            {
                @this.Final.Volume = @this.Final.Volume ?? new Volume();
                @this.Final.Pricing = @this.Final.Pricing ?? new JobPricing();
            }
            
        }
    }
}
