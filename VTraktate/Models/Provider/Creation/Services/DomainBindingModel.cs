using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Models.Helpers;

namespace VTraktate.Models.Provider.Creation.Services
{
    public class DomainBindingModel
    {
        public IdNamePairBindingModel Domain { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}