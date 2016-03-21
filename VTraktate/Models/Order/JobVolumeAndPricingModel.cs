using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order
{
    public class JobVolumeAndPricingModel
    {
        public VolumeModel Volume { get; set; }
        public JobPricingModel Pricing { get; set; }
    }
}