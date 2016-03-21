using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models.Order
{
    public class JobPricingModel
    {
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}