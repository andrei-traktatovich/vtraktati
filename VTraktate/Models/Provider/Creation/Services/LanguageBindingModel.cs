using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;

namespace VTraktate.Models.Provider.Creation.Services
{
    public class LanguageBindingModel
    {
        public IdNamePairBindingModel LanguagePair { get; set; }
        public int Stars { get; set; }
        public DomainBindingModel[] Domains { get; set; }
        public bool Native { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public string Comment { get; set; }

        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }
    }
}