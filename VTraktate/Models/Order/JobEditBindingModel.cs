using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order
{
    public class JobEditBindingModel : JobCreateModel
    {
        public JobVolumeAndPricingModel Final { get; set; }
    }
}