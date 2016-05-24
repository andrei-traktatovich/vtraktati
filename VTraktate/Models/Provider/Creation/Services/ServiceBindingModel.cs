using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;

namespace VTraktate.Models.Provider.Creation.Services
{
    public class ServiceBindingModel
    {
        public IdNamePairBindingModel Type { get; set; }
        public int Stars { get; set; }

        public string Comment { get; set; }
        public int CurrencyId { get; set; }
        public int UomId { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public bool IsLinguistic { get; set; }
        public LanguageBindingModel[] Languages { get; set; }

    }
}