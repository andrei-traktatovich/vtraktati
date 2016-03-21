using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.Domain.ComplexTypes
{
    [ComplexType]
    public class JobPricing : PricingBase
    {
        public int Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
