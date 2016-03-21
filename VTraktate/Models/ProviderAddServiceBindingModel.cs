using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.Models
{
    public class ProviderAddServiceBindingModel
    {
        public int ServiceId { get; set; }
        public int? Stars { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public int UomId { get; set; }
        public int CurrencyId { get; set; }
        public string Comment { get; set; }
        public int CreatedById { get; set; }

    }
}